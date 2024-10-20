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
    public class OrderHeaderRepository : Repositery<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDBContext context;

		public OrderHeaderRepository(ApplicationDBContext context) : base(context)
		{
			this.context = context;
		}


		public void Update(OrderHeader orderHeader)
		{
			context.orderHeaders.Update(orderHeader);
		}

	}
}
