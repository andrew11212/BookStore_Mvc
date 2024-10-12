using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Models
{
	public class CartViewModel
	{
		public IEnumerable<ShopingCart> CartList { get; set; }

		public double TotalOrder { get; set; }
	}
}
