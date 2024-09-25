using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class CategoryRepository : Repositery<Category>, ICategoryRepository
	{
		private readonly ApplicationDBContext context;

		public CategoryRepository(ApplicationDBContext context) : base(context)
		{
			this.context = context;
		}


		public void Update(Category category)
		{
			context.Categories.Update(category);
		}

	}
}
