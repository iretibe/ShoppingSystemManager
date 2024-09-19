namespace ShoppingSystem.Product.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; } = default!;
        public List<string> Category { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public string ProductImage { get; set; } = default!;
        public string Status { get; set; } //Active or Inactive
        public double InitialStock { get; set; } 
        public double ReorderLevel { get; set; }
        public double StockLevel { get; set; } 
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid StoreId { get; set; } //Reference to StoreId from Store Service
    }
}
