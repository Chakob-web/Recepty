using Hejl_RecipeWebApplication.Entities;
using System.ComponentModel.DataAnnotations;


namespace Hejl_RecipeWebApplication.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Název je povinný.")]
        [StringLength(100, ErrorMessage = "Název může mít maximálně 100 znaků.")]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ingredience jsou povinné.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Instrukce jsou povinné.")]
        public string Instructions { get; set; }

        [Url(ErrorMessage = "Zadejte platnou URL adresu.")]
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
