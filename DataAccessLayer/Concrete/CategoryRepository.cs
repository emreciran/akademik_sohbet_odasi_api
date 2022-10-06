using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            var deletedCategory = await GetByID(id);
            db.Categories.Remove(deletedCategory);
            await db.SaveChangesAsync();
        }

        public async Task<Category> GetByID(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await db.Categories.Where(x => x.Category_Name == categoryName).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Category>> GetCategoryByProject(int projectID)
        {
            var categories = await db.ProjectCategories
                .Where(x => x.Project_ID == projectID)
                .Select(x => x.Category)
                .ToListAsync();

            return categories;
        }

        public async Task<List<Category>> GetListAll()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> Insert(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return category;
        }
    }
}
