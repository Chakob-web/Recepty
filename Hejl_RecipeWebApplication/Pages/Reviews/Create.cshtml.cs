
using System;
using System.Linq;
using System.Threading.Tasks;
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hejl_RecipeWebApplication.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Recipe? Recipe { get; private set; }

        [BindProperty]
        public Review Review { get; set; } = new Review();

        public IActionResult OnGet(int recipeId)
        {
            Recipe = _context.Recipes.FirstOrDefault(r => r.Id == recipeId);

            if (Recipe == null)
            {
                return NotFound();
            }

            Review.RecipeId = recipeId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Review.CreatedAt = DateTime.UtcNow;
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}


