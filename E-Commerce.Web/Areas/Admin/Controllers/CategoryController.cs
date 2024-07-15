using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("[area]/[controller]/Category-list")]
        public IActionResult Index()
        {
            List<Category> category = _unitOfWork.Category.GetAll().ToList();
            return View(category);
        }
        [HttpGet]
        [Route("[area]/[controller]/Add-Category")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Route("[area]/[controller]/Add-Category")]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        [Route("[area]/[controller]/Update-Category/{Id}")]
        public IActionResult EditCategory(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [Route("[area]/[controller]/Update-Category/{Id}")]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category Update Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("[area]/[controller]/Delete-Category/{Id}")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        [Route("[area]/[controller]/Delete-Category/{Id}")]
        public IActionResult DeleteCategoryPost(int? id)
        {
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["Success"] = "Category Delete Successfully!";
            return RedirectToAction("Index");
        }
    }
}
