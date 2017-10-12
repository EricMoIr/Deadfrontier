﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = "");
        T GetByID(object id);
        void Insert(T entityToCreate);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        Type GetEntityType();
    }
}