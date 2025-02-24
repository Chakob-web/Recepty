using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using System.Linq;

namespace Hejl_RecipeWebApplication.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; }

        public IActionResult OnGet(int id)
        {
            Review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (Review == null)
            {
                return NotFound();
            }

            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost(int id)
        {
            Review = _context.Reviews.Find(id);

            if (Review != null)
            {
                _context.Reviews.Remove(Review);
                _context.SaveChanges();
            }

            return RedirectToPage("/Recipes/Detail", new { id = Review.RecipeId });
        }
    }
}
