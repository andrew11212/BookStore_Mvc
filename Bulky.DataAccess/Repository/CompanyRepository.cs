using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repositery<Company>, ICompanyRepository
    {

        private readonly ApplicationDBContext context;

        public CompanyRepository(ApplicationDBContext _context) : base(_context)
        {
            {
                this.context = _context;
            }

            
        }

		public void Update(Company company)
		{
			context.Update(company);
		}
	}
}
