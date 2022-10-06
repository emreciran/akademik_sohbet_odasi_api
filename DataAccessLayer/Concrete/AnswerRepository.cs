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

    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext db;

        public AnswerRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            var deletedAnswer = await GetByID(id);
            db.Answers.Remove(deletedAnswer);
            await db.SaveChangesAsync();
        }

        public async Task<Answer> GetByID(int id)
        {
            return await db.Answers.FindAsync(id);
        }

        public async Task<List<Answer>> GetAnswerByQuestion(int questionId)
        {
            var answers = await db.Answers
                .Where(x => x.Question_ID == questionId)
                .Include(x => x.Question).ToListAsync();

            return answers;
        }

        public async Task<List<Answer>> GetListAll()
        {
            return await db.Answers.ToListAsync();
        }

        public async Task<Answer> Insert(Answer answer)
        {
            answer.CreatedDate = DateTime.Now;
            db.Answers.Add(answer);
            await db.SaveChangesAsync();
            return answer;
        }

        public async Task<Answer> Update(Answer answer)
        {
            db.Answers.Update(answer);
            await db.SaveChangesAsync();
            return answer;
        }
    }
}
