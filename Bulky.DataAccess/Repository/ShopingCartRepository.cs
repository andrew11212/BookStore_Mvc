using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ShopingCartRepository : Repositery<ShopingCart>, IShopingCartRepositery
	{
		private readonly ApplicationDBContext context;

		public ShopingCartRepository(ApplicationDBContext context) : base(context)
		{
			this.context = context;
		}


		public void Update(ShopingCart shopingCart)
		{
			context.shopingCarts.Update(shopingCart);
		}

	}
}
