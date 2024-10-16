namespace ShoppingSystem.BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message) { }

        public string? _details { get; }

        public InternalServerException(string message, string details) : base(message)
        {
            _details = details;
        }
    }
}
