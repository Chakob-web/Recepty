using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Receptar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Recipe>? Recipes { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}