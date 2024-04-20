using E_Commerce.DataAccess.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Category/Category-list")]
        public IActionResult Index()
        {
            List<Category> category = _context.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        [Route("Category/Add-Category")]    
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Route("Category/Add-Category")]
        public IActionResult CreateCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        [Route("Category/Update-Category/{Id}")]
        public IActionResult EditCategory(int id)
        {
            if(id==0 || id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [Route("Category/Update-Category/{Id}")]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["Success"] = "Category Update Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("Category/Delete-Category/{Id}")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        [Route("Delete-Category/{Id}")]
        public IActionResult DeleteCategoryPost(int? id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Category Delete Successfully!";
            return RedirectToAction("Index");
        }
    }
}
