using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DomainModels
{
   public class Delivery : BaseEntity 
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime EstimatedDeliveryTime { get; set; }
        public string Status { get; set; } = "Preparing";
    }
}
