using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDBContext context;
		public ICategoryRepository CategoryRepository { get; private set; }

		public IProductRepository ProductRepository { get; private set; }

		public ICompanyRepository CompanyRepository { get; private set; }

		public IShopingCartRepositery CartRepository { get; private set; }
		
		public IApplciationUserRepository applciationUserRepository { get; private set; }
		public IOrderHeaderRepository OrderHeader { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }

		public IShopingCartRepositery CartRepositery { get; private set; }

		public UnitOfWork(ApplicationDBContext context)
		{
			this.context = context;
			CategoryRepository = new CategoryRepository(context);

			ProductRepository = new ProductRepository(context);

			CompanyRepository = new CompanyRepository(context);

			CartRepository = new ShopingCartRepository(context);
			applciationUserRepository = new ApplciationUserRepository(context);
			CartRepositery = new ShopingCartRepository(context);
			OrderHeader = new OrderHeaderRepository(context);
			OrderDetail = new OrderDetailRepository(context);
		}
		public void Save()
		{
			context.SaveChanges();
		}

	}
}
