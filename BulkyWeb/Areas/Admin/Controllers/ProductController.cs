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
		public ActionResult edit(int id) 
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

		public ActionResult edit(Product product) 
		{
			if (ModelState.IsValid) 
			{
				unitOfWork.ProductRepository.Ubdate(product);
				unitOfWork.Save();

				return RedirectToAction("Index");
			}
			return View(product);

		}
	}
}
