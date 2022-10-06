using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.Delete(id);
        }

        public async Task<Category> GetByID(int id)
        {
            return await _categoryRepository.GetByID(id);
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _categoryRepository.GetCategoryByName(categoryName);
        }

        public async Task<ICollection<Category>> GetCategoryByProject(int projectID)
        {
            return await _categoryRepository.GetCategoryByProject(projectID);
        }

        public async Task<List<Category>> GetListAll()
        {
            return await _categoryRepository.GetListAll();
        }

        public async Task<Category> Insert(Category category)
        {
            return await _categoryRepository.Insert(category);
        }

        public async Task<Category> Update(Category category)
        {
            return await _categoryRepository.Update(category);
        }
    }
}
