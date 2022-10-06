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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext db;

        public QuestionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Question> AddQuestion(int[] tagIds, Question question)
        {
            foreach (var tagId in tagIds)
            {
                var tag = db.Tags.Where(x => x.Tag_ID == tagId).FirstOrDefault();

                var questionTag = new QuestionTag()
                {
                    Tag = tag,
                    Question = question
                };

                db.QuestionTags.Add(questionTag);
            }
            question.CreatedDate = DateTime.Now;

            db.Questions.Add(question);
            await db.SaveChangesAsync();
            return question;
        }

        public async Task Delete(int id)
        {
            var deletedQuestion = await GetByID(id);
            db.Questions.Remove(deletedQuestion);
            await db.SaveChangesAsync();
        }

        public async Task<Question> GetByID(int id)
        {
            return await db.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetListAll()
        {
            return await db.Questions.ToListAsync();
        }

        public Task<Question> Insert(Question t)
        {
            throw new NotImplementedException();
        }

        public async Task<Question> Update(Question question)
        {
            db.Questions.Update(question);
            await db.SaveChangesAsync();
            return question;
        }
    }
}
