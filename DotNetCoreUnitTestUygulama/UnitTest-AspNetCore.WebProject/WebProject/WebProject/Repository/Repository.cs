using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SQLitePCL;
using WebProject.Models;

namespace WebProject.Repository
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:class
    {
        private readonly UnitTestExampleContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(UnitTestExampleContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;  //bu şekilde update yapılırsa örneğin 20 property içeiren bir entity var ve biz bir tanesini güncelledik, db tarafına o 20 tane prop'u güncelleyecek sorgu gönderilir.
                                                                //ve performans kaybı yaşanır.
                                                                //eğer klasik yöntemle yani id ile entity bulunup sadece güncellenmek istenen alan maplenirse savechanges metodu dbye sadece ilgili alanların güncellenmesini sağlar.
                                                                //ilk yöntem mapping olarak kod azlığı sağlar ama performans kaybı yaşatır
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
