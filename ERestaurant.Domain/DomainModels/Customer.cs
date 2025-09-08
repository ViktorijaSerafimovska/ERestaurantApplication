using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DomainModels
{
   public class Customer : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LasttName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

    }
}
