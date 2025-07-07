namespace Reservation.Features.Order.GetAllOrder.Dtos;

public record GetOrderResponseDto(string RequesterName, string RequesterPhoneNom,
                                    string RequesterNationalCode, DateOnly FromDate,
                                    DateOnly ToDate, long RoomId , string RoomName);

