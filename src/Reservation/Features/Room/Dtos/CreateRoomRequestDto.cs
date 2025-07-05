using FluentValidation;

namespace Reservation.Features.Room.Dtos;

public record CreateRoomRequestDto(string Name);

//public class CreateRoomRequestDtoValidatator : AbstractValidator<CreateRoomRequestDto>
//{
//    public CreateRoomRequestDtoValidatator()
//    {
//        RuleFor(a => a.Name).NotEmpty().WithMessage("نام اتاق نباید خالی باشد");
//    }
//}

