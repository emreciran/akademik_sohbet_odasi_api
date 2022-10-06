using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class QuestionManager : IQuestionService
    {
        IQuestionRepository _questionRepository;

        public QuestionManager(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> AddQuestion(int[] tagIds, Question question)
        {
            return await _questionRepository.AddQuestion(tagIds, question);
        }

        public async Task Delete(int id)
        {
            await _questionRepository.Delete(id);
        }

        public async Task<Question> GetByID(int id)
        {
            return await _questionRepository.GetByID(id);
        }

        public async Task<List<Question>> GetListAll()
        {
            return await _questionRepository.GetListAll();
        }

        public async Task<Question> Insert(Question t)
        {
            throw new NotImplementedException();
        }

        public async Task<Question> Update(Question t)
        {
            return await _questionRepository.Update(t);
        }
    }
}
