using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AnswerManager : IAnswerService
    {
        IAnswerRepository _answerRepository;

        public AnswerManager(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task Delete(int id)
        {
            await _answerRepository.Delete(id);
        }

        public async Task<List<Answer>> GetAnswerByQuestion(int questionId)
        {
            return await _answerRepository.GetAnswerByQuestion(questionId);
        }

        public async Task<Answer> GetByID(int id)
        {
            return await _answerRepository.GetByID(id);
        }

        public async Task<List<Answer>> GetListAll()
        {
            return await _answerRepository.GetListAll();
        }

        public async Task<Answer> Insert(Answer t)
        {
            return await _answerRepository.Insert(t);
        }

        public async Task<Answer> Update(Answer t)
        {
            return await _answerRepository.Update(t);
        }
    }
}
