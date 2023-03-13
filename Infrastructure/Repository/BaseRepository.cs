using AutoMapper.Execution;
using Domain.Cloth;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using static Dapper.SqlMapper;

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
        public (IEnumerable<T> data, int total) Get(string? include, Expression<Func<T, bool>>? filter, int? pageSize, int? page)
        {
            using (var db = new MyDbContext())
            {
                IQueryable<T> rs = db.Set<T>();
                if (include!=null)
                {
                    rs = rs.Include(include);
                }
                if (filter != null)
                    rs = rs.Where(filter);
                var total = rs.Count();
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
