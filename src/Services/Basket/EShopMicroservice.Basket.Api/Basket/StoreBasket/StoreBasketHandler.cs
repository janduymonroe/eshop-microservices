using EShopMicroservice.Discount.Grpc;

namespace EShopMicroservice.Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasketAsync(command.Cart);
        
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellation)
    {
        var tasks = cart.Items.Select(async x =>
        {
            var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = x.ProductName });
            x.Price -= coupon.Amount;
        });

        await Task.WhenAll(tasks);
    }
}

