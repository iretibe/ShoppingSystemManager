using MediatR;

namespace ShoppingSystem.Store.API.Stores.CreateStore
{
    public record CreateStoreCommand(string StoreName, string Location, string ContactNumber,
        string Status, string Owner, string Address, string City, string CreateBy, DateTime CreateDate) : IRequest<CreateStoreResult>;

    public record CreateStoreResult(Guid Id);

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CreateStoreResult>
    {
        public async Task<CreateStoreResult> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
