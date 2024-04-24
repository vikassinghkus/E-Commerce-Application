using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.product.GetAll().ToList();
            return View(product);
        }
        [HttpGet]
        [Route("[area]/[controller]/Create-Product")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("[area]/[controller]/Create-Product")]
        public IActionResult Create(Product obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("[area]/[controller]/Update-Product/{id}")]
        public IActionResult Edit(int id)
        {
            if(id ==0 || id == null)
            {
                return NotFound();
            }
            Product product = _unitOfWork.product.Get(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [Route("[area]/[controller]/Update-Product/{id}")]
        public IActionResult Edit(Product obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("[area]/[controller]/Delete-Product/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product product = _unitOfWork.product.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost,ActionName("Delete")]
        [Route("[area]/[controller]/Delete-Product/{id}")]
        public IActionResult DeletePost(int id)
        {
            Product product = _unitOfWork.product.Get(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            _unitOfWork.product.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product Delete Successfully!";
            return RedirectToAction("Index");
        }
    }
}
