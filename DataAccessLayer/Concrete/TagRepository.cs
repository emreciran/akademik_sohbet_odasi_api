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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext db;

        public TagRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            var deletedTag = await GetByID(id);
            db.Tags.Remove(deletedTag);
            await db.SaveChangesAsync();
        }

        public async Task<Tag> GetByID(int id)
        {
            return await db.Tags.FindAsync(id);
        }

        public async Task<List<Tag>> GetListAll()
        {
            return await db.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await db.Tags.Where(x => x.TagName == tagName).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Tag>> GetTagByQuestion(int questionId)
        {
            var tags = await db.QuestionTags
                .Where(x => x.Question_ID == questionId)
                .Select(x => x.Tag)
                .ToListAsync();

            return tags;
        }

        public async Task<Tag> Insert(Tag tag)
        {
            db.Tags.Add(tag);
            await db.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> Update(Tag tag)
        {
            db.Tags.Update(tag);
            await db.SaveChangesAsync();
            return tag;
        }
    }
}
