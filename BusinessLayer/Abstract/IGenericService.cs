using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        Task<T> Insert(T t);

        Task Delete(int id);

        Task<T> Update(T t);

        Task<List<T>> GetListAll();

        Task<T> GetByID(int id);
    }
}
