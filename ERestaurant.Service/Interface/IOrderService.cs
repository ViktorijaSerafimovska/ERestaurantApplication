using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;

namespace ERestaurant.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrder(Guid id);
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        void DeleteOrder(Guid id);
        Order PlaceOrder(Guid customerId, List<Guid> menuItemIds);
        void AddOrderItem(OrderItem item);
    }
}
