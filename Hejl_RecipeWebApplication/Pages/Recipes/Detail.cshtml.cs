using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hejl_RecipeWebApplication.Entities;
using Hejl_RecipeWebApplication.Data;
using System.Collections.Generic;
using System.Linq;

namespace Hejl_RecipeWebApplication.Pages.Recipes
{
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

        public IActionResult OnGet(int id)
        {
            Recipe = _context.Recipes.Include(r => r.Reviews).FirstOrDefault(r => r.Id == id);
            if (Recipe == null)
            {
                return NotFound();
            }

            Reviews = _context.Reviews
                .Where(r => r.RecipeId == id)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return Page();
        }
    }
}
