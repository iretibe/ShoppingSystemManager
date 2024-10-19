using Microsoft.Extensions.Caching.Distributed;
using ShoppingSystem.Basket.API.Models;
using ShoppingSystem.Basket.API.Repositories;
using System.Text.Json;

namespace ShoppingSystem.Basket.API.Cache
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache distributedCache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            await repository.DeleteBasketAsync(userName, cancellationToken);

            await distributedCache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);

            if(!string.IsNullOrEmpty(cachedBasket)) 
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);

            var basket = await repository.GetBasketAsync(userName, cancellationToken);
            
            await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);

            await distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket),cancellationToken);

            return basket;
        }
    }
}
