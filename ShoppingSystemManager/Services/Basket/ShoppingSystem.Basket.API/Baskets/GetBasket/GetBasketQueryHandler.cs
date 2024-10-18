using ShoppingSystem.Basket.API.Models;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Basket.API.Baskets.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart cart);

    public class GetBasketQueryHandler(IBasketRepository _repo) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //Get basket from database
            var basket = await _repo.GetBasketAsync(request.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}
