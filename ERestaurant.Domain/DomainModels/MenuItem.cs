using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DomainModels
{
    public class MenuItem : BaseEntity
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

    }
}
