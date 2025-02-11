using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hejl_RecipeWebApplication.Entities;
using Hejl_RecipeWebApplication.Data;

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

        public IActionResult OnGet(int id)
        {
            Recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (Recipe == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
