using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]  // Specify that this controller is part of the Admin area
	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public ProductController(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}

		
		public IActionResult Index()
		{
			var productList= unitOfWork.ProductRepository.GetAll().ToList();
			return View(productList);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Product product)
		{
			if (ModelState.IsValid)
			{
				unitOfWork.ProductRepository.Add(product);
				unitOfWork.Save();
				TempData["Success"] = "Category Created successfully";
				return RedirectToAction("Index");
			}
			return View(product);
		}

		public IActionResult Delete(int id)
		{
			var product = unitOfWork.ProductRepository.Get(p => p.Id == id);
			if (id == 0)
			{
				return NotFound();
			}
			return View(product);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var product = unitOfWork.ProductRepository.Get(p => p.Id == id);
			if (ModelState.IsValid)
			{
				unitOfWork.ProductRepository.Remove(product);
				unitOfWork.Save();
				TempData["Success"] = "Category Deleted successfully";

				return RedirectToAction(nameof(Index));
			}
			return View();
		}
		[HttpGet]
		public ActionResult Edit(int id) 
		{
			var product = unitOfWork.ProductRepository.Get(e=>e.Id==id);

			if (product == null) 
			{
				return NotFound();
			}
			return View(product);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult Edit(Product product) 
		{
			if (ModelState.IsValid) 
			{
				unitOfWork.ProductRepository.Ubdate(product);
				unitOfWork.Save();
				TempData["Success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View(product);

		}
	}
}
