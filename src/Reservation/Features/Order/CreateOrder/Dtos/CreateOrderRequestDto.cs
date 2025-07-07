namespace Reservation.Features.Order.CreateOrder.Dtos;

public record CreateOrderRequestDto(string RequesterName , string RequesterPhoneNom, 
                                    string RequesterNationalCode , DateOnly FromDate,
                                    DateOnly ToDate , long RoomId);
