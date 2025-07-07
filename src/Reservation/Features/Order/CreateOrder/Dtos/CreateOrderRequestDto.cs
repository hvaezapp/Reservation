using FluentValidation;

namespace Reservation.Features.Order.CreateOrder.Dtos;

public record CreateOrderRequestDto(string RequesterName, string RequesterPhoneNom,
                                    string RequesterNationalCode, DateOnly FromDate,
                                    DateOnly ToDate, long RoomId);


public class CreateOrderRequestDtoValidatator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestDtoValidatator()
    {

        RuleFor(x => x.RequesterName)
             .NotEmpty()
             .WithMessage("Requester name is required.")
             .MaximumLength(100)
             .WithMessage("Requester name must not exceed 100 characters.");


        RuleFor(x => x.RequesterPhoneNom)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{11}$")
            .WithMessage("Phone number must be exactly 11 digits without country code.");


        RuleFor(x => x.RequesterNationalCode)
            .NotEmpty()
            .WithMessage("National code is required.")
            .Length(10)
            .WithMessage("National code must be exactly 10 digits.")
            .Matches(@"^\d{10}$")
            .WithMessage("National code must contain only digits.");


        RuleFor(x => x)
             .Must(x => x.FromDate < x.ToDate)
             .WithMessage("FromDate must be earlier than ToDate.");

    }
}