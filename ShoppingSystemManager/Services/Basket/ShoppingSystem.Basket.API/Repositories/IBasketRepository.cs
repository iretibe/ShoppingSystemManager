using ShoppingSystem.Basket.API.Models;

namespace ShoppingSystem.Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken);
        Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken);
    }
}
