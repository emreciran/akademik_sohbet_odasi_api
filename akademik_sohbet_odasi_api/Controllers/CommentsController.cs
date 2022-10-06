using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private ICommentRepository _commentRepository;
        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var comments = await _commentRepository.GetListAll();
            return Ok(comments); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetByID(id);
            if (comment != null)
            {
                return Ok(comment);
            }
            return NotFound(); //404
        }

        [HttpGet]
        [Route("question/{questionId}")]
        public async Task<IActionResult> GetCommentByQuestion(int questionId)
        {
            var asnwers = await _commentRepository.GetCommentByQuestion(questionId);
            if (asnwers != null)
            {
                return Ok(asnwers);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            var createdComment = await _commentRepository.Insert(comment);
            return CreatedAtAction("GetAllComment", new { id = createdComment.ID }, createdComment);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            if (_commentRepository.GetByID(comment.ID) != null)
            {
                var updatedComment = await _commentRepository.Update(comment);
                return Ok(updatedComment);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (await _commentRepository.GetByID(id) != null)
            {
                await _commentRepository.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
