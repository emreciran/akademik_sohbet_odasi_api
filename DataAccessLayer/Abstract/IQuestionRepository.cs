using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<Question> AddQuestion(int[] tagIds, Question question);
    }
}
