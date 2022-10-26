using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    //Benoy this  will allow all paroperties inside the UI bind automatically  . for example OnPost we dont need to pass the category parameter 
    [BindProperties]
    public class CreateModel : PageModel
    {
        //Benoy this  will allow Category property to be avilable in the form . for example OnPost we dont need to pass the category parameter 
       //[BindProperty]
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult>OnPost()
        {
            var catry = _db.Category.Where(p => (p.DisplayOrder == Category.DisplayOrder));
            if (catry?.Any() == true)
            {

                ModelState.AddModelError("displayorder", "Cannot use same order");
            }


            if (Category.Name == Category.DisplayOrder.ToString())
            {
                //Benoy since we are using Key as name error didplys both summary and below name field
                ModelState.AddModelError("Category.Name", "The DisplayOrder Cannot match Name");
            }

            if (ModelState.IsValid)
            {
                await _db.Category.AddAsync(Category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
