using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}
		public IActionResult Index()
		{
			var category = categoryRepository.GetAll();
			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "the Display Order cannot exactly match The name");
			}
			if (ModelState.IsValid)
			{
				categoryRepository.Add(category);
				categoryRepository.Save();
				TempData["Success"] = "Category updated successfully";
				return RedirectToAction(nameof(Index));  // Redirect back to the list after saving
			}
			return View(category); // Return view with model if validation fails
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{

			var category = categoryRepository.Get(e=>e.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category); // Return the view with the category data
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			 if (ModelState.IsValid)
			{

				var categoryIndb = categoryRepository.Get(c=>c.Id == category.Id);
				// or categoryRepository.Update(category); 
				if (categoryIndb != null)
				{
					categoryIndb.Name = category.Name;
					categoryIndb.DisplayOrder = category.DisplayOrder;
					categoryRepository.Save();
					TempData["Success"] = "Category updated successfully";
					return RedirectToAction(nameof(Index));
				}
				return NotFound();
			}
			return View(category);
		}
		
		public IActionResult Delete(int id)
		{
			var category = categoryRepository.Get(c=>c.Id==id);
			if (category == null)
			{
				return NotFound(); // Return 404 if the category is not found
			}

			return View(category); // Return the confirmation view
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]	
		public IActionResult DeleteConfirmed(int id) 
		{
			var CategoryIndb= categoryRepository.Get(c => c.Id == id);
			if (CategoryIndb != null) 
			{
				categoryRepository.Remove(CategoryIndb);
				categoryRepository.Save();
				TempData["Success"] = "Category Deleted successfully";
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
	}
}
