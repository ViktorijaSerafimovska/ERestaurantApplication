using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;
using ERestaurant.Repository;
using ERestaurant.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ERestaurant.Service.Implementation
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<Delivery> _deliveryRepository;
        private readonly ApplicationDbContext _context;

        public DeliveryService(IRepository<Delivery> deliveryRepository, ApplicationDbContext context)
        {
            _deliveryRepository = deliveryRepository;
            _context = context;
        }

        public Delivery GetDelivery(Guid id)
        {
            return _deliveryRepository.Get(x => x, predicate: d => d.Id == id);
        }

        public Delivery CreateDelivery(Delivery delivery)
        {
            return _deliveryRepository.Insert(delivery);
        }

        public Delivery UpdateDelivery(Delivery delivery)
        {
            return _deliveryRepository.Update(delivery);
        }

        public void DeleteDelivery(Guid id)
        {
            var delivery = GetDelivery(id);
            if (delivery != null)
                _deliveryRepository.Delete(delivery);
        }

        public List<Delivery> GetAllDeliveries()
        {
            return _context.Deliveries
                .Include(d => d.Order)
                .ThenInclude(o => o.Customer)
                .ToList();
        }

      
    }
}
