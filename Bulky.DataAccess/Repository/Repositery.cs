using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class Repositery<T>:IRepository<T> where T : class
	{
		private readonly ApplicationDBContext context;

		internal DbSet<T> Set;
		public Repositery(ApplicationDBContext context)
        {
			this.context = context;
			this.Set=context.Set<T>();
			//Categories = Dbset;
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = Set;
			return query.ToList();
		}

		public T Get(Expression<Func<T, bool>> Filter)
		{

			IQueryable<T> query = Set;

			query = query.Where(Filter);
			return query.FirstOrDefault();
		}

		public void Add(T entity)
		{
			Set.Add(entity);
		}

		public void Remove(T entity)
		{
			Set.Remove(entity);
		}

		public void RemoveRamge(IEnumerable<T> entity)
		{
			Set.RemoveRange(entity);
		}
	}
}
