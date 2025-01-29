using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hejl_RecipeWebApplication.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            try
            {
                _context.Recipes.Add(Recipe);
                await _context.SaveChangesAsync();
                Console.WriteLine("Recept pøidán do databáze: " + Recipe.Title);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Došlo k chybì pøi ukládání dat: " + ex.ToString());
                Console.WriteLine(ex.ToString());
                return Page();
            }

        }

    }
}
