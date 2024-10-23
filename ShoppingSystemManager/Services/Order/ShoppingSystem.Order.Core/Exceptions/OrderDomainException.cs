namespace ShoppingSystem.Order.Core.Exceptions
{
    public class OrderDomainException : Exception
    {
        public OrderDomainException(string message) : base($"Domain Exception: {message} throws from domain layer")
        {
            
        }
    }
}
