using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IQuestionService : IGenericService<Question>
    {
        Task<Question> AddQuestion(int[] tagIds, Question question);
    }
}
