using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using ERestaurant.Repository;

namespace ERestaurant.Service.Implementation
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;

        public MenuItemService(IRepository<MenuItem> menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuItemRepository.GetAll(x => x).ToList();
        }

        public MenuItem GetMenuItem(Guid id)
        {
            return _menuItemRepository.Get(x => x, predicate: mi => mi.Id == id);
        }

        public MenuItem CreateMenuItem(MenuItem item)
        {
            return _menuItemRepository.Insert(item);
        }

        public MenuItem UpdateMenuItem(MenuItem item)
        {
            return _menuItemRepository.Update(item);
        }

        public void DeleteMenuItem(Guid id)
        {
            var item = GetMenuItem(id);
            if (item != null)
                _menuItemRepository.Delete(item);
        }
    }
}
