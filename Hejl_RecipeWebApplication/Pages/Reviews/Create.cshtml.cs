<<<<<<< HEAD
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

=======

using System;
using System.Linq;
using System.Threading.Tasks;
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
>>>>>>> 469bafb5111b7e63040755ef74c1f1ac47e8d051

namespace Hejl_RecipeWebApplication.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
        [BindProperty]
        public Review Review { get; set; }

        public void OnGet()
        {
            Review = new Review();
=======
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
>>>>>>> 469bafb5111b7e63040755ef74c1f1ac47e8d051
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

<<<<<<< HEAD
            if (string.IsNullOrWhiteSpace(Review.Author))
            {
                Review.Author = "Guest";
            }

=======
            Review.CreatedAt = DateTime.UtcNow;
>>>>>>> 469bafb5111b7e63040755ef74c1f1ac47e8d051
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
<<<<<<< HEAD
=======


>>>>>>> 469bafb5111b7e63040755ef74c1f1ac47e8d051
