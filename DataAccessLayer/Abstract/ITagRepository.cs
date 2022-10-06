using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<ICollection<Tag>> GetTagByQuestion(int questionId);

        Task<Tag> GetTagByName(string tagName);
    }
}
