﻿
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public void Add(T entity)
        {
            using (var db = new MyDbContext())
            {
                db.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var db = new MyDbContext())
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }
        public (IEnumerable<T> data, int total) Get(Expression<Func<T, object>>[]? includeProperties, Expression<Func<T, bool>>? filter, int? pageSize, int? page)
        {
            using (var db = new MyDbContext())
            {
                IQueryable<T> rs = db.Set<T>();
                if (filter != null)
                    rs = rs.Where(filter);
                var total = rs.Count();
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        rs = rs.Include(includeProperty);
                    }
                }
                rs = rs.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
                return (rs.ToList(), total);
            }
        }


        public void Update(int id, T entity)
        {
            using (var db = new MyDbContext())
            {

                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
