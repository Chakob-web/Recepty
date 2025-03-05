using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hejl_RecipeWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();

        public async Task OnGetAsync()
        {
            Recipes = await _context.Recipes
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    Description = r.Description,
                    AverageRating = _context.Reviews
                        .Where(review => review.RecipeId == r.Id)
                        .Select(review => (double?)review.Rating)
                        .Average() ?? 0
                })
                .ToListAsync();
        }
    }

    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double AverageRating { get; set; }
    }
}
