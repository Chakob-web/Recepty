using System.Linq;
using System.Threading.Tasks;
using Hejl_RecipeWebApplication.Data;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hejl_RecipeWebApplication.Pages.Reviews
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; } = new Review();

        public IActionResult OnGet(int id)
        {
            Review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (Review == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var reviewInDb = _context.Reviews.FirstOrDefault(r => r.Id == Review.Id);
            if (reviewInDb == null)
            {
                return NotFound();
            }

            reviewInDb.Rating = Review.Rating;
            reviewInDb.Comment = Review.Comment;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipes/Detail", new { id = reviewInDb.RecipeId});
        }
    }
}
