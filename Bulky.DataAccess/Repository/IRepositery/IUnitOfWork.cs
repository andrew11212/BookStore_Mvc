using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepositery
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		IProductRepository ProductRepository { get; }

		public ICompanyRepository CompanyRepository { get;  }

		public IShopingCartRepositery CartRepositery { get; }

		public IApplciationUserRepository applciationUserRepository { get;  }
		public IOrderHeaderRepository OrderHeader { get;  }
		public IOrderDetailRepository OrderDetail { get;  }
		void Save();
	}
}
