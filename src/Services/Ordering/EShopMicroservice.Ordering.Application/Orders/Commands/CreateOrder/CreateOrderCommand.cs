using FluentValidation;

namespace EShopMicroservice.Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithErrorCode("1001")
            .WithMessage("OrderName is required");

        RuleFor(x => x.Order.CustomerId)
            .NotEmpty()
            .WithErrorCode("1002")
            .WithMessage("CustomerId is required");

        RuleFor(x => x.Order.OrderItems)
            .NotEmpty()
            .WithErrorCode("1003")
            .WithMessage("OrderItems should not be empty");
    }
}