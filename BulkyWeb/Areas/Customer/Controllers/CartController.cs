using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
	[Authorize]
	[Area("Customer")]
	public class CartController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public CartViewModel CartVM { get; set; } = default!;
		public CartController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}



		public IActionResult Index()
		{
			var Claims = User.Identity as ClaimsIdentity;
			var UserId = Claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			CartVM = new()
			{
				CartList = unitOfWork.CartRepositery.GetAll(c => c.ApplicationUserId == UserId, "Product")

			};
			foreach (var cart in CartVM.CartList)
			{
				cart.price = GetPriceBasedOnQuantity(cart);
				CartVM.TotalOrder += (cart.price * cart.Count); 
			}
			return View(CartVM);
		}

		private double GetPriceBasedOnQuantity(ShopingCart shopingCart)
		{
			if (shopingCart.Count <=50)
			{
				return shopingCart.Product.Price;
			}
			else if (shopingCart.Count <= 100)
			{ return shopingCart.Product.Price50; }

			else { return shopingCart.Product.Price100; };
		}
	}
}
