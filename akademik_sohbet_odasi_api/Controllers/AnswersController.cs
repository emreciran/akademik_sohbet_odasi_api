using BusinessLayer.Abstract;
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
    public class AnswersController : ControllerBase
    {
        private IAnswerService _answerService;

        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswer()
        {
            var answers = await _answerService.GetListAll();
            return Ok(answers); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var asnwer = await _answerService.GetByID(id);
            if (asnwer != null)
            {
                return Ok(asnwer);
            }
            return NotFound(); //404
        }

        [HttpGet]
        [Route("question/{questionId}")]
        public async Task<IActionResult> GetAnswerByQuestion(int questionId)
        {
            var asnwers = await _answerService.GetAnswerByQuestion(questionId);
            if (asnwers != null)
            {
                return Ok(asnwers);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            var createdAnswer = await _answerService.Insert(answer);
            return CreatedAtAction("GetAllQuestion", new { id = createdAnswer.Answer_ID }, createdAnswer);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(Answer answer)
        {
            if (_answerService.GetByID(answer.Answer_ID) != null)
            {
                var updatedAnswer = await _answerService.Update(answer);
                return Ok(updatedAnswer);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            if (await _answerService.GetByID(id) != null)
            {
                await _answerService.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
