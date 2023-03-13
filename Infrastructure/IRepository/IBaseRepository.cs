﻿using Domain.Cloth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    }
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(T entity);
       IEnumerable<T> Get(string? include, Expression<Func<T, bool>>? filter, int? pageSize, int? page);
}
