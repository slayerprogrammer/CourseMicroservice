using Courses.Services.Order.Domain.Core;

namespace Courses.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductUrl { get; private set; }
        public Decimal Price { get; private set; }

        // DDD üzerinde tek başına eklenmesin diye kapatıyoruz
        // bizim Order ımız AggragateRoot olacak
        //1:N ilişki için tabloda tanımı olacak fakat burada property olmayacak
        //bunlara shadow property deniliyor
        //public int OrderId { get; private set; } => buna verilen ad Navigation Property

        public OrderItem()
        {
        }

        public OrderItem(string productId, string productName, string productUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }

        public void updateOrderItem(string productName, string productUrl, decimal price)
        {
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
    }
}