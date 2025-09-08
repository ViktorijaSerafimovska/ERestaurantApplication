using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DomainModels
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }
    }
}
