using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;

namespace ERestaurant.Service.Interface
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItem(Guid id);
        MenuItem CreateMenuItem(MenuItem item);
        MenuItem UpdateMenuItem(MenuItem item);
        void DeleteMenuItem(Guid id);
    }
}
