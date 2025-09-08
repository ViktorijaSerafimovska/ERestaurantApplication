using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERestaurant.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IOrderService _orderService;
        private readonly IWeatherService _weatherService;

        public DeliveryController(IDeliveryService deliveryService, IOrderService orderService, IWeatherService weatherService)
        {
            _deliveryService = deliveryService;
            _orderService = orderService;
            _weatherService = weatherService;
        }

        // Index + Weather
        public async Task<IActionResult> Index()
        {
            var deliveries = _deliveryService.GetAllDeliveries();
            var weather = await _weatherService.GetWeatherAsync("Skopje");

            ViewBag.WeatherDescription = weather.Weather.FirstOrDefault()?.Description;
            ViewBag.WeatherIcon = weather.Weather.FirstOrDefault()?.Icon;
            ViewBag.Temp = weather.Main.Temp;

            if (weather.Weather.Any(w => w.Main.ToLower().Contains("rain")))
                ViewBag.DeliveryMessage = "🚚 Delivery might take longer due to rain ☔";
            else
                ViewBag.DeliveryMessage = "🚚 Delivery is on time ✅";

            return View(deliveries);
        }

        // Details
        public IActionResult Details(Guid id)
        {
            var delivery = _deliveryService.GetDelivery(id);
            if (delivery == null) return NotFound();
            return View(delivery);
        }

        // GET: Create
        public IActionResult Create()
        {
            PopulateOrdersDropdown();
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid orderId, string deliveryAddress, DateTime estimatedDeliveryTime, string status)
        {
            if (orderId == Guid.Empty)
            {
                ModelState.AddModelError("", "You must select an order.");
            }

            if (ModelState.IsValid)
            {
                var delivery = new Delivery
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    DeliveryAddress = deliveryAddress,
                    EstimatedDeliveryTime = estimatedDeliveryTime,
                    Status = string.IsNullOrEmpty(status) ? "Preparing" : status
                };

                _deliveryService.CreateDelivery(delivery);
                return RedirectToAction(nameof(Index));
            }

            PopulateOrdersDropdown(orderId);
            return View();
        }

        // GET: Edit
        public IActionResult Edit(Guid id)
        {
            var delivery = _deliveryService.GetDelivery(id);
            if (delivery == null) return NotFound();

            PopulateOrdersDropdown(delivery.OrderId);
            return View(delivery);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,OrderId,DeliveryAddress,EstimatedDeliveryTime,Status")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _deliveryService.UpdateDelivery(delivery);
                return RedirectToAction(nameof(Index));
            }

            PopulateOrdersDropdown(delivery.OrderId);
            return View(delivery);
        }

        // GET: Delete
        public IActionResult Delete(Guid id)
        {
            var delivery = _deliveryService.GetDelivery(id);
            if (delivery == null) return NotFound();
            return View(delivery);
        }

        // POST: Delete
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _deliveryService.DeleteDelivery(id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateOrdersDropdown(Guid? selectedOrderId = null)
        {
            var orders = _orderService.GetAllOrders();
            ViewBag.Orders = new SelectList(
                orders.Select(o => new
                {
                    Id = o.Id,
                    Display = $"{o.Customer.FirstName} {o.Customer.LasttName} - {o.CreatedAt:dd/MM/yyyy} - {o.Status}"
                }),
                "Id", "Display", selectedOrderId);
        }
    }
}
