using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAnswerService : IGenericService<Answer>
    {
        Task<List<Answer>> GetAnswerByQuestion(int questionId);
    }
}
