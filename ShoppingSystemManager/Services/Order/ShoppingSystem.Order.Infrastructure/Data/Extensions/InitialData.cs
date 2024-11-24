using ShoppingSystem.Order.Core.Models;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("17B59B45-F996-4C24-AED5-5BC142EB052F")), "Mawuli Mensah", "mawulim@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("C0C100B6-8851-4648-B979-8ACF603C4C58")), "Joseph Paintsil", "joseph.p@gmail.com")
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("DCE11641-4CE0-4650-AFAA-EAE37FAC84A9")), "BELLA SPAGHETTI B/S", 455),
                Product.Create(ProductId.Of(new Guid("DD2127E7-D13F-4D80-BCF8-EEAA7C65A274")), "MAMA SPAGHETTI B/S", 470),
                Product.Create(ProductId.Of(new Guid("EE8F5602-B116-4A2B-9739-CA707492A705")), "LELE SPAGHETTI B/S", 455),
                Product.Create(ProductId.Of(new Guid("6D126FD9-8156-4FF3-BDE0-D4489BA114EF")), "CHERIE NOODLES", 350)
            };

        public static IEnumerable<Core.Models.Order> OrderWithItems
        {
            get
            {
                var address1 = Address.Of("Mawuli", "Mensah", "mawulim@gmail.com", "Achimota New Market", "Ghana", "Greater Accra", "00233");
                var address2 = Address.Of("Joseph", "Paintsil", "joseph.p@gmail.com", "Teshie", "Ghana", "Greater Accra", "00233");

                var payment1 = Payment.Of("Mawuli", "123456789032", "01/27", "123", 1);
                var payment2 = Payment.Of("Joseph", "908765431276", "01/29", "345", 2);

                var order1 = Core.Models.Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("17B59B45-F996-4C24-AED5-5BC142EB052F")),
                    OrderName.Of("ORD_1"),
                    Address.Of("Mawuli", "Mensah", "mawulim@gmail.com", "Achimota New Market", "Ghana", "Greater Accra", "00233"),
                    Address.Of("Joseph", "Paintsil", "joseph.p@gmail.com", "Teshie", "Ghana", "Greater Accra", "00233"),
                    Payment.Of("Mawuli", "123456789032", "01/27", "123", 1));
                order1.Add(ProductId.Of(new Guid("DCE11641-4CE0-4650-AFAA-EAE37FAC84A9")), 5, 455);
                order1.Add(ProductId.Of(new Guid("DD2127E7-D13F-4D80-BCF8-EEAA7C65A274")), 3, 470);


                var order2 = Core.Models.Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("C0C100B6-8851-4648-B979-8ACF603C4C58")),
                    OrderName.Of("ORD_2"),
                    Address.Of("Mawuli", "Mensah", "mawulim@gmail.com", "Achimota New Market", "Ghana", "Greater Accra", "00233"),
                    Address.Of("Joseph", "Paintsil", "joseph.p@gmail.com", "Teshie", "Ghana", "Greater Accra", "00233"),
                    Payment.Of("Joseph", "908765431276", "01/29", "345", 2));
                order2.Add(ProductId.Of(new Guid("EE8F5602-B116-4A2B-9739-CA707492A705")), 5, 455);
                order2.Add(ProductId.Of(new Guid("6D126FD9-8156-4FF3-BDE0-D4489BA114EF")), 6, 350);

                return new List<Core.Models.Order> { order1, order2 };
            }
        }
    }
}
