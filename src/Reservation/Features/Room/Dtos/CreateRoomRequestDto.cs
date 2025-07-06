using FluentValidation;

namespace Reservation.Features.Room.Dtos;

public record CreateRoomRequestDto(string name);

//public class CreateRoomRequestDtoValidatator : AbstractValidator<CreateRoomRequestDto>
//{
//    public CreateRoomRequestDtoValidatator()
//    {
//        RuleFor(a => a.Name).NotEmpty().WithMessage("Room Name cannot be null or empty");
//    }
//}

