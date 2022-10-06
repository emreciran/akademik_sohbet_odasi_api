using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentRepository _commentRepository;

        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Delete(int id)
        {
            await _commentRepository.Delete(id);
        }

        public async Task<Comment> GetByID(int id)
        {
            return await _commentRepository.GetByID(id);
        }

        public async Task<List<Comment>> GetCommentByQuestion(int questionId)
        {
            return await _commentRepository.GetCommentByQuestion(questionId);
        }

        public async Task<List<Comment>> GetListAll()
        {
            return await _commentRepository.GetListAll();
        }

        public async Task<Comment> Insert(Comment t)
        {
            return await _commentRepository.Insert(t);
        }

        public async Task<Comment> Update(Comment t)
        {
            return await _commentRepository.Update(t);
        }
    }
}
