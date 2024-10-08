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
	public class Repositery<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDBContext context;

		internal DbSet<T> Set;
		public Repositery(ApplicationDBContext context)
		{
			this.context = context;
			this.Set = context.Set<T>();
			//Categories = Dbset;
		}

		public IQueryable<T> GetAll(params string[] includes)
		{
			IQueryable<T> query = Set;
			foreach (var include in includes) 
			{
				query = query.Include(include);
			}
			return query;
		}

		public T Get(Expression<Func<T, bool>> Filter, params string[] includes)
		{
			IQueryable<T> query = Set;

			query = query.Where(Filter);

			// Apply string-based includes
			foreach (var include in includes)
			{
				query = query.Include(include);
			}

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
