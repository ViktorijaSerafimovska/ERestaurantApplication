using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ERestaurant.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: /Customer
        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        // GET: /Customer/Details/5
        public IActionResult Details(Guid id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // GET: /Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: /Customer/Edit/5
        public IActionResult Edit(Guid id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: /Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: /Customer/Delete/5
        public IActionResult Delete(Guid id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: /Customer/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
