using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepositery
{
	public interface IRepository <T> where T : class
	{
		IEnumerable<T> GetAll (params string[] includes);

		T Get (Expression<Func<T,bool>> Filter, params string[] includes);

		 void Add(T entity);
		
		void Remove(T entity);

		void RemoveRamge(IEnumerable<T> entity);


	}
}
