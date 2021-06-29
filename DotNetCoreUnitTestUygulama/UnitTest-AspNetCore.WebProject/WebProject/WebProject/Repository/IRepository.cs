﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Repository
{
    //unit testte mock kavramının uygulanması için projede DI prensibiyle birlikte soyutlamanın iyi bir şekilde uygulanması gerekir
   public interface IRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
