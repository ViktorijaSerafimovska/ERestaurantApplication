using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DomainModels
{
   public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual Delivery? Delivery { get; set; }
    }
}
