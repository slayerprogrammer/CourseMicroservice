using Courses.Services.Order.Domain.Core;

namespace Courses.Services.Order.Domain.OrderAggregate
{
    //EF Core Features
    //-Owned Typed
    //-Shadow Property
    //-Backing Field
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        //backing fields
        //public olsaydı data eklenirdi bu nedenle private

        private readonly List<OrderItem> _orderItems;

        //dışarıya readonly olarak açmak için yazdık
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}