namespace ShoppingSystem.Store.API.Models
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string StoreName { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public string Status { get; set; } // Active, Inactive
        public string Owner { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
    }
}
