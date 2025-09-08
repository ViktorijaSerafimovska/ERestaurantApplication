using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ERestaurant.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IMenuItemService _menuItemService;

        public OrderController(IOrderService orderService, ICustomerService customerService, IMenuItemService menuItemService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _menuItemService = menuItemService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        public IActionResult Details(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null) return NotFound();
            return View(order);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.MenuItems = _menuItemService.GetAllMenuItems();
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid customerId, List<Guid> menuItemIds)
        {
            if (menuItemIds == null || !menuItemIds.Any())
            {
                ModelState.AddModelError("", "You must select at least one menu item.");
            }

            if (ModelState.IsValid)
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
                foreach (var menuId in menuItemIds)
                {
                    var menuItem = _menuItemService.GetMenuItem(menuId);
                    if (menuItem != null)
                    {
                        order.OrderItems.Add(new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            MenuItemId = menuItem.Id,
                            Quantity = 1
                        });
                        total += menuItem.Price;
                    }
                }

                order.TotalPrice = total;
                _orderService.CreateOrder(order);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.MenuItems = _menuItemService.GetAllMenuItems();
            return View();
        }

        // GET: Edit
        public IActionResult Edit(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null) return NotFound();

            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.MenuItems = _menuItemService.GetAllMenuItems();
            return View(order);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Guid customerId, List<Guid> menuItemIds)
        {
            var order = _orderService.GetOrder(id);
            if (order == null) return NotFound();

            if (menuItemIds == null || !menuItemIds.Any())
            {
                ModelState.AddModelError("", "You must select at least one menu item.");
            }

            if (ModelState.IsValid)
            {
                order.CustomerId = customerId;

                if (order.OrderItems != null)
                {
                    foreach (var oi in order.OrderItems.ToList())
                    {
                        _orderService.DeleteOrder(oi.Id);
                    }
                }

                order.OrderItems = new List<OrderItem>();
                decimal total = 0;
                foreach (var menuId in menuItemIds)
                {
                    var menuItem = _menuItemService.GetMenuItem(menuId);
                    if (menuItem != null)
                    {
                        var orderItem = new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            MenuItemId = menuItem.Id,
                            Quantity = 1
                        };
                        total += menuItem.Price;
                        _orderService.AddOrderItem(orderItem);
                        order.OrderItems.Add(orderItem);
                    }
                }

                order.TotalPrice = total;
                _orderService.UpdateOrder(order);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.MenuItems = _menuItemService.GetAllMenuItems();
            return View(order);
        }

        public IActionResult Delete(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
