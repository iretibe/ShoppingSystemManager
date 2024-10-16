namespace ShoppingSystem.BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string? message) : base(message)
        {
        }

        public BadRequestException(string? message, string details) : base(message)
        {
            _details = details;
        }

        public string? _details { get; }
    }
}
