using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class ProductRepository : Repositery<Product>,IProductRepository
	{
		private readonly ApplicationDBContext context;

        public ProductRepository(ApplicationDBContext context):base(context) 
        {
			this.context = context;
		}
        public void Ubdate(Product product)
		{
			context.Update(product);
		}
		
	}

}
