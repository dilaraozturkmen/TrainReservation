using FluentValidation;

namespace TrainBooking.Application.BookingOperations.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(command => command.Model.TrainName).NotEmpty();
            RuleFor(command => command.Model.NumberOfPersonsToReservation).NotEmpty();
            RuleFor(command => command.Model.wagon).NotEmpty();

        }

    }
}
