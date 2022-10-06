using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<ICollection<Category>> GetCategoryByProject(int projectID);

        Task<Category> GetCategoryByName(string categoryName);
    }
}
