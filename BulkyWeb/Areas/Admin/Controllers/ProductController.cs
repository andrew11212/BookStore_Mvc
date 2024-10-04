using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]  // Specify that this controller is part of the Admin area
	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment webHostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			this.unitOfWork = unitOfWork;
			this.webHostEnvironment = webHostEnvironment;
		}


		public IActionResult Index()
		{
			var productList = unitOfWork.ProductRepository.GetAll().Include(c => c.Category).ToList();
			return View(productList);
		}

		[HttpGet]
		public IActionResult Ubsert(int? id)
		{
			// Initialize ProductViewModel
			ProductViewModel productVM = new ProductViewModel()
			{
				product = new Product(),
				CategoryList = unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				})
			};

			// Set ViewBag.Title based on the action (Create or Update)
			if (id == null || id == 0)
			{
				ViewBag.Title = "Create Product";
				return View(productVM);  // For Create
			}
			else
			{
				ViewBag.Title = "Update Product";
				productVM.product = unitOfWork.ProductRepository.Get(p => p.Id == id.GetValueOrDefault());
				if (productVM.product == null)
				{
					return NotFound();
				}
				return View(productVM);  // For Update
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Ubsert(ProductViewModel productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = webHostEnvironment.WebRootPath;

				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, "Images", "Product");

					if (!string.IsNullOrEmpty(productVM.product.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.product.ImageUrl.TrimStart('/').Replace("/", "\\"));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}

					}

					using (FileStream fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}


					productVM.product.ImageUrl = Path.Combine("/Images/Product/", fileName).Replace("\\", "/");
				}


				if (productVM.product.Id == 0)
				{
					unitOfWork.ProductRepository.Add(productVM.product);
					TempData["Success"] = "Product Created Successfully";
				}
				else
				{
					unitOfWork.ProductRepository.Ubdate(productVM.product);
					TempData["Success"] = "Product Updated Successfully";
				}

				unitOfWork.Save();

				return RedirectToAction("Index");
			}

			return View(productVM);
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
		public IActionResult GetAll()
		{

			var products = unitOfWork.ProductRepository.GetAll();
			var result = products.Select(p => new
			{
				title = p.Title,
				isbn = p.ISBN,
				listPrice = p.ListPrice,
				author=p.Author,
				price = p.Price,
				price50 = p.Price50,
				price100 = p.Price100,
				category = new { name = p.Category.Name }
			}).ToList();

			return Json(new { data = result });

		}
	}
}
