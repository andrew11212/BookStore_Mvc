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
    public class OrderDetailRepository : Repositery<OrderDetail>,IOrderDetailRepository
	{
		private readonly ApplicationDBContext context;

		public OrderDetailRepository(ApplicationDBContext context) : base(context)
		{
			this.context = context;
		}


		public void Update(OrderDetail order)
		{
			context.orderDetails.Update(order);
		}

	}
}
