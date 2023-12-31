﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderDataLayer.Repository
{
    public interface IRepository<T , D>
    {
        T GetById(int id);
        List<T> GetAll();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);

    }
}
