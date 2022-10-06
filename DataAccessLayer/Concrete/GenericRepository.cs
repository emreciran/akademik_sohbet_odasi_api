using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;

        public GenericRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            db.Remove<T>(await GetByID(id));
            await db.SaveChangesAsync();
        }

        public async Task<T> GetByID(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetListAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> Insert(T t)
        {
            db.Set<T>().Add(t);
            await db.SaveChangesAsync();
            return t;
        }

        public async Task<T> Update(T t)
        {
            db.Set<T>().Update(t);
            await db.SaveChangesAsync();
            return t;
        }
    }
}
