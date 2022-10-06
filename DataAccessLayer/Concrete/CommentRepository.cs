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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext db;

        public CommentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(int id)
        {
            var deletedComment = await GetByID(id);
            db.Comments.Remove(deletedComment);
            await db.SaveChangesAsync();
        }

        public async Task<Comment> GetByID(int id)
        {
            return await db.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetCommentByQuestion(int questionId)
        {
            var comments = await db.Comments
                .Where(x => x.Question_ID == questionId)
                .Include(x => x.Question).ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetListAll()
        {
            return await db.Comments.ToListAsync();
        }

        public async Task<Comment> Insert(Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> Update(Comment comment)
        {
            db.Comments.Update(comment);
            await db.SaveChangesAsync();
            return comment;
        }
    }
}
