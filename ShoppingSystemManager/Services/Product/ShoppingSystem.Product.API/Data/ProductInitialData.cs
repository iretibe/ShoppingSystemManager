using Marten;
using Marten.Schema;

namespace ShoppingSystem.Product.API.Data
{
    public class ProductInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Models.Product>().AnyAsync()) return;

            //Marten UPSERT will cater for existing records
            session.Store<Models.Product>(GetPreConfiguredProducts());

            await session.SaveChangesAsync();
        }

        private static IEnumerable<Models.Product> GetPreConfiguredProducts() => new List<Models.Product>
        {
            new Models.Product()
            {
                Id = Guid.NewGuid(),
                ProductCode = "PR" + Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 5),
                Barcode = string.Empty,
                ProductName = "A.A.A RICE 50 KG",
                Category = ["RICE"],
                ProductDescription = "This is a rice product",
                ProductImage = string.Empty,
                Status = "Active",
                InitialStock = 0,
                ReorderLevel = 0,
                StockLevel = 1000,
                CostPrice = 745,
                SellingPrice = 780,
                CreateBy = string.Empty,
                CreateDate = DateTime.Now,
                StoreId = Guid.Parse(Guid.NewGuid().ToString())
            },
            new Models.Product()
            {
                Id = Guid.NewGuid(),
                ProductCode = "PR" + Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 5),
                Barcode = string.Empty,
                ProductName = "A.A.A. RICE 25KG",
                Category = ["RICE"],
                ProductDescription = "This is a rice product",
                ProductImage = string.Empty,
                Status = "Active",
                InitialStock = 0,
                ReorderLevel = 0,
                StockLevel = 1000,
                CostPrice = 570,
                SellingPrice = 585,
                CreateBy = string.Empty,
                CreateDate = DateTime.Now,
                StoreId = Guid.Parse(Guid.NewGuid().ToString())
            },
            new Models.Product()
            {
                Id = Guid.NewGuid(),
                ProductCode = "PR" + Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 5),
                Barcode = string.Empty,
                ProductName = "ABUBURO KOSUA RICE 50KG",
                Category = ["RICE"],
                ProductDescription = "This is a rice product",
                ProductImage = string.Empty,
                Status = "Active",
                InitialStock = 0,
                ReorderLevel = 0,
                StockLevel = 1000,
                CostPrice = 720,
                SellingPrice = 760,
                CreateBy = string.Empty,
                CreateDate = DateTime.Now,
                StoreId = Guid.Parse(Guid.NewGuid().ToString())
            },
            new Models.Product()
            {
                Id = Guid.NewGuid(),
                ProductCode = "PR" + Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 5),
                Barcode = string.Empty,
                ProductName = "ACCRA STAR FLOUR 50KG",
                Category = ["FLOUR"],
                ProductDescription = "This is a flour product",
                ProductImage = string.Empty,
                Status = "Active",
                InitialStock = 0,
                ReorderLevel = 0,
                StockLevel = 1000,
                CostPrice = 870,
                SellingPrice = 880,
                CreateBy = string.Empty,
                CreateDate = DateTime.Now,
                StoreId = Guid.Parse(Guid.NewGuid().ToString())
            }
        };
    }
}
