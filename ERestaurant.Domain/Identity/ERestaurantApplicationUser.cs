using Microsoft.AspNetCore.Identity;

namespace ERestaurant.Domain.Identity
{
    public class ERestaurantApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
    }
}
