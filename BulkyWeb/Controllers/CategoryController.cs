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
			if (ModelState.IsValid)
			{
				_Context.Categories.Add(category);
				_Context.SaveChanges();
				return RedirectToAction(nameof(Index));  // Redirect back to the list after saving
			}
			return View(category); // Return view with model if validation fails
		}
	}
}
