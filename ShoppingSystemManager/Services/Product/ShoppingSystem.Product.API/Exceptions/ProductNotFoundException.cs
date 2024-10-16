using ShoppingSystem.BuildingBlocks.Exceptions;

namespace ShoppingSystem.Product.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base ("Product", Id)
        {
            
        }
    }
}
