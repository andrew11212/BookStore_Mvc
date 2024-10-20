using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepositery
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader> 
	{
		void Update(OrderHeader orderHeader);

	}
}
