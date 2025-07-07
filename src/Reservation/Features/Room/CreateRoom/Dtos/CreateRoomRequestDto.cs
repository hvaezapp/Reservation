using FluentValidation;

namespace Reservation.Features.Room.CreateRoom.Dtos;

public record CreateRoomRequestDto(string Name);

public class CreateRoomRequestDtoValidatator : AbstractValidator<CreateRoomRequestDto>
{
    public CreateRoomRequestDtoValidatator()
    {
        RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name cannot be null or empty");

    }
}

