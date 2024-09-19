using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDBContext _Context;
		public CategoryController(ApplicationDBContext context)
		{
			_Context = context;
		}
		public IActionResult Index()
		{
			var category = _Context.Categories.ToList();
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
				_Context.Categories.Add(category);
				_Context.SaveChanges();
				TempData["Success"] = "Category updated successfully";
				return RedirectToAction(nameof(Index));  // Redirect back to the list after saving
			}
			return View(category); // Return view with model if validation fails
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{

			var category = _Context.Categories.FirstOrDefault(c => c.Id == id);
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

				var categoryIndb = _Context.Categories.FirstOrDefault(c => c.Id == category.Id);
				// or _Context.Categories.Update(category); 
				if (categoryIndb != null)
				{
					categoryIndb.Name = category.Name;
					categoryIndb.DisplayOrder = category.DisplayOrder;
					_Context.SaveChanges();
					TempData["Success"] = "Category updated successfully";
					return RedirectToAction(nameof(Index));
				}
				return NotFound();
			}
			return View(category);
		}
		
		public IActionResult Delete(int id)
		{
			var category = _Context.Categories.FirstOrDefault(c => c.Id == id);
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
			var CategoryIndb= _Context.Categories.FirstOrDefault(c=> c.Id == id);
			if (CategoryIndb != null) 
			{
				_Context.Categories.Remove(CategoryIndb);
				_Context.SaveChanges();
				TempData["Success"] = "Category Deleted successfully";
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
	}
}
