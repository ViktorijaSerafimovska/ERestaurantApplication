using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using ERestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace ERestaurant.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<MenuItem> menuItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _menuItemRepository = menuItemRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll(
                selector: x => x,
                include: x => x.Include(o => o.OrderItems)
                               .ThenInclude(oi => oi.MenuItem)
                               .Include(o => o.Customer)
                               .Include(o => o.Delivery)
            ).ToList();
        }

        public Order GetOrder(Guid id)
        {
            return _orderRepository.Get(
                selector: x => x,
                predicate: o => o.Id == id,
                include: x => x.Include(o => o.OrderItems)
                               .ThenInclude(oi => oi.MenuItem)
                               .Include(o => o.Customer)
                               .Include(o => o.Delivery)
            );
        }
        public void AddOrderItem(OrderItem item)
        {
            _orderItemRepository.Insert(item);
        }

        public Order CreateOrder(Order order) => _orderRepository.Insert(order);

        public Order UpdateOrder(Order order) => _orderRepository.Update(order);

        public void DeleteOrder(Guid id)
        {
            var order = GetOrder(id);
            if (order != null)
                _orderRepository.Delete(order);
        }

        public Order PlaceOrder(Guid customerId, List<Guid> menuItemIds)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;
            foreach (var menuItemId in menuItemIds)
            {
                var menuItem = _menuItemRepository.Get(x => x, predicate: mi => mi.Id == menuItemId);
                if (menuItem != null)
                {
                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        MenuItemId = menuItemId,
                        Quantity = 1
                    };
                    total += menuItem.Price;
                    _orderItemRepository.Insert(orderItem);
                    order.OrderItems.Add(orderItem);
                }
            }

            order.TotalPrice = total;
            return _orderRepository.Insert(order);
        }
    }
}
