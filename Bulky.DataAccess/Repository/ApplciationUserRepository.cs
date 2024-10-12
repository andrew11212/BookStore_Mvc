using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ApplciationUserRepository : Repositery<ApplicationUser>,IApplciationUserRepository
	{
		private readonly ApplicationDBContext context;

		public ApplciationUserRepository(ApplicationDBContext context) : base(context)
		{
			this.context = context;
		}

	}
}
