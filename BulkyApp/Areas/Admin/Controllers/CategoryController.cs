using Bulky.DataAcess.Repositoryy.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        public CategoryController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unityOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
           
        }
        public IActionResult Create()
        {
            return View();
           
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "the displayorder cannot exactly match the name.");
            }
            if (ModelState.IsValid) {
                _unityOfWork.Category.Add(obj);
                _unityOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index","Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _unityOfWork.Category.Get(u=>u.Id==id);
            //Category? CategoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? CategoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Update(obj);
                _unityOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _unityOfWork.Category.Get(u=>u.Id==id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unityOfWork.Category.Get(u=>u.Id==id);

            if (obj == null)
            {
                return NotFound();
            }

            _unityOfWork.Category.Remove(obj);
            _unityOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
