using BulkyWeb.Data;
using Microsoft.AspNetCore.Mvc;

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
	}
}
