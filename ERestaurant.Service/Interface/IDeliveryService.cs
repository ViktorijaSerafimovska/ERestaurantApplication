using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;

namespace ERestaurant.Service.Interface
{
    public interface IDeliveryService
    {
        List<Delivery> GetAllDeliveries();
        Delivery GetDelivery(Guid id);
        Delivery CreateDelivery(Delivery delivery);
        Delivery UpdateDelivery(Delivery delivery);
        void DeleteDelivery(Guid id);
    }
}
