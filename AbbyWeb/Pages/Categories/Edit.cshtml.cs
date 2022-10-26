using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    //Benoy this  will allow all paroperties inside the UI bind automatically  . for example OnPost we dont need to pass the category parameter 
    [BindProperties]
    public class EditModel : PageModel
    {
        //Benoy this  will allow Category property to be avilable in the form . for example OnPost we dont need to pass the category parameter 
       //[BindProperty]
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            
            //if (id == null || id == 0)
            //{
            //    return NotFound();
            //}
            Category = _db.Category.Find(id);
            // var CategoryFromDBFirst = _db.Categories.FirstDefault(u => u.id == id);
            // var CategoryFromDBSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            //if (Category == null)
            //{
            //    return NotFound();
            //}
            //return Page(Category);
        }

        public async Task<IActionResult>OnPost()
        {
           
            var catry = _db.Category.Where(p => (p.id != Category.id) && (p.DisplayOrder == Category.DisplayOrder));

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
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
