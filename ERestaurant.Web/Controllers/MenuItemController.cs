using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ERestaurant.Web.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public IActionResult Index() => View(_menuItemService.GetAllMenuItems());

        public IActionResult Details(Guid id)
        {
            var item = _menuItemService.GetMenuItem(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuItem item)
        {
            if (ModelState.IsValid)
            {
                _menuItemService.CreateMenuItem(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Edit(Guid id)
        {
            var item = _menuItemService.GetMenuItem(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MenuItem item)
        {
            if (ModelState.IsValid)
            {
                _menuItemService.UpdateMenuItem(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Delete(Guid id)
        {
            var item = _menuItemService.GetMenuItem(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _menuItemService.DeleteMenuItem(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
