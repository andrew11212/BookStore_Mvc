using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepositery
{
    public interface IProductRepository:IRepository<Product> 
	{
		void Ubdate(Product product);
	}
}
