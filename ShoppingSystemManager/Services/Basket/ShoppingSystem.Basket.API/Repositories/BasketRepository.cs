using Marten;
using ShoppingSystem.Basket.API.Exceptions;
using ShoppingSystem.Basket.API.Models;

namespace ShoppingSystem.Basket.API.Repositories
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);

            return basket;
        }
    }
}
