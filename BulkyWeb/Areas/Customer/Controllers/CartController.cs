using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
	[Authorize]
	[Area("Customer")]
	public class CartController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		[BindProperty]
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
				CartList = unitOfWork.CartRepositery.GetAll(c => c.ApplicationUserId == UserId, "Product"),
				OrderHeader=new()
			};
			foreach (var cart in CartVM.CartList)
			{
				cart.price = GetPriceBasedOnQuantity(cart);
				CartVM.OrderHeader.OrderTotal += (cart.price * cart.Count); 
			}
			return View(CartVM);
		}
		public IActionResult Summary()
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			CartVM = new()
			{
				CartList = unitOfWork.CartRepositery.GetAll(u => u.ApplicationUserId == userId,"Product"),
				OrderHeader = new()
			};
			CartVM.OrderHeader.ApplicationUser = unitOfWork.applciationUserRepository.Get(u => u.Id == userId);

			CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.Name;
			CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.ApplicationUser.PhoneNumber;
			CartVM.OrderHeader.Address = CartVM.OrderHeader.ApplicationUser.StreetAddress;
			CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
			CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;
			CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.ApplicationUser.PostalCode;



			foreach (var cart in CartVM.CartList)
			{
				cart.price = GetPriceBasedOnQuantity(cart);
				CartVM.OrderHeader.OrderTotal += (cart.price * cart.Count);
			}
			return View(CartVM);
		}
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			CartVM.CartList = unitOfWork.CartRepositery.GetAll(u => u.ApplicationUserId == userId, "Product");

			CartVM.OrderHeader.OrderDate = System.DateTime.Now;
			CartVM.OrderHeader.ApplicationUserId = userId;

			ApplicationUser applicationUser = unitOfWork.applciationUserRepository.Get(u => u.Id == userId);


			foreach (var cart in CartVM.CartList)
			{
				cart.price = GetPriceBasedOnQuantity(cart);
				CartVM.OrderHeader.OrderTotal += (cart.price * cart.Count);
			}

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				//it is a regular customer 
				CartVM.OrderHeader.PaymentStatus = SD.Payment_Status_Pending;
				CartVM.OrderHeader.OrderStatus = SD.Status_Pending;
			}
			else
			{
				//it is a company user
				CartVM.OrderHeader.PaymentStatus = SD.Payment_Status_Delayed_Payment;
				CartVM.OrderHeader.OrderStatus = SD.Status_Approved;
			}
			unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
			unitOfWork.Save();
			foreach (var cart in CartVM.CartList)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderId = CartVM.OrderHeader.Id,
					Price = cart.price,
					Count = cart.Count
				};
				unitOfWork.OrderDetail.Add(orderDetail);
				unitOfWork.Save();
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
