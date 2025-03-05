using Hejl_RecipeWebApplication.Entities;
using System.ComponentModel.DataAnnotations;


namespace Hejl_RecipeWebApplication.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RecipeId { get; set; }

        public virtual Recipe? Recipe { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = "Guest";
    }
}
