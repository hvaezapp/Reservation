using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RedLockNet.SERedis;
using Reservation.Domain.Entities;
using Reservation.Features.Order.CreateOrder.Dtos;
using Reservation.Features.Order.CreateOrder.Endpoint;
using Reservation.Features.Order.GetAllOrder.Dtos;
using Reservation.Infrastructure.Persistence.Context;
using System.Text.Json;

namespace Reservation.Features.Order.Services
{
    public class OrderService(ReservationDbContext reservationDbContext, IValidator<CreateOrderRequestDto> validator, RedLockFactory redLockFactory)
    {
        private readonly ReservationDbContext _reservationDbContext = reservationDbContext;
        public readonly IValidator<CreateOrderRequestDto> _validator = validator;
        private readonly RedLockFactory _redLockFactory = redLockFactory;

        #region lock prerequired
        public const string CreateOrderDistributedLockPattern = "reservation:order:create:{0}";
        public static TimeSpan ExpireTime = TimeSpan.FromSeconds(20);
        public static TimeSpan WaitTime = TimeSpan.FromSeconds(5);
        public static TimeSpan RetryTime = TimeSpan.FromSeconds(3);
        #endregion


        public async Task Create(CreateOrderRequestDto dto, CancellationToken cancellationToken)
        {
            var resource = string.Format(CreateOrderDistributedLockPattern, dto.RequesterNationalCode);

            await using var redLock = await _redLockFactory.CreateLockAsync(resource, ExpireTime, WaitTime, RetryTime, cancellationToken);
            if (!redLock.IsAcquired)
                throw new Exception("Unexpected Error");


            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ArgumentException($"Validation Failed , Message => {validationResult.Errors.Select(s => s.ErrorMessage).First()}", nameof(dto));


            var selectedRoom = await _reservationDbContext.Rooms.FindAsync(dto.RoomId, cancellationToken);

            if (selectedRoom is null)
                throw new Exception("Selected Room Not Found, RoomId Invalid");

            if (selectedRoom.IsReserved)
                throw new Exception("Requester Can't Reserve this Room, Becuase this Room Reserved Before");

            try
            {
                // begin transaction
                await _reservationDbContext.Database.BeginTransactionAsync(cancellationToken);

                var newOrder = Domain.Entities.Order.Create(dto.RequesterName, dto.RequesterPhoneNom,
                                                            dto.RequesterEmail, dto.RequesterNationalCode,
                                                            dto.FromDate, dto.ToDate, dto.RoomId);


                // add new order to db
                await _reservationDbContext.Orders.AddAsync(newOrder, cancellationToken);

                // set selected room to reserve status
                selectedRoom.Reserve();



                var notificationEvent = new NotificationEvent(dto.RequesterPhoneNom,
                                                              dto.RequesterEmail,
                                                              TemplateType.ReservationCompleted);

                // add to Outbox table
                await _reservationDbContext.Outboxs.AddAsync(new Outbox
                {
                    Message = JsonSerializer.Serialize(notificationEvent),
                    EventType = EventType.Notification,
                    CreatedOn = DateTime.UtcNow,

                }, cancellationToken);


                await _reservationDbContext.SaveChangesAsync(cancellationToken);
                await _reservationDbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                // rollBack
                await _reservationDbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw new Exception("Reservation process failed!");
            }
        }

        public async Task<IEnumerable<GetOrderResponseDto>> GetAll(CancellationToken cancellationToken)
        {
            return (await _reservationDbContext.Orders.Include(a => a.Room).Select(a =>
                    new GetOrderResponseDto
                    (
                        a.RequesterName,
                        a.RequesterPhoneNom,
                        a.RequesterNationalCode,
                        a.FromDate,
                        a.ToDate,
                        a.RoomId,
                        a.Room.Name

                    )).ToListAsync(cancellationToken));
        }
    }
}
