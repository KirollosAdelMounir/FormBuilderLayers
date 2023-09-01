using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FormBuilderDataLayer.Repository
{
    public class Repository<T , D> : IRepository<T,D> where T : class where D : DbContext
    {
        private readonly DbContext _dbContext;
        private DbSet<T> _entity;
        
        public Repository(D d) 
        {
            _dbContext = d;
            _entity = _dbContext.Set<T>();  
            
        }
        public async Task AddAsync(T entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
             await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }
       
        public async Task<T> GetById(int id)
        {
            var entity = await _entity.FindAsync(id);
            if(entity!= null)
            {
                return entity;
            }
            return null;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
