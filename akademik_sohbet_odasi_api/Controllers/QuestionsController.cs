using akademik_sohbet_odasi_api.Dto;
using AutoMapper;
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
    public class QuestionsController : ControllerBase
    {
        private IQuestionService _questionService;
        private IMapper _mapper;

        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestion()
        {
            var questions = _mapper.Map<List<QuestionDto>>(await _questionService.GetListAll());
            return Ok(questions); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = _mapper.Map<QuestionDto>(await _questionService.GetByID(id));
            if (question != null)
            {
                return Ok(question);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromQuery] int[] tagIds, [FromBody] Question question)
        {
            var createdQuestion = await _questionService.AddQuestion(tagIds, question);
            return CreatedAtAction("GetAllQuestion", new { id = createdQuestion.Question_ID }, createdQuestion);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            if (_questionService.GetByID(question.Question_ID) != null)
            {
                var updatedQuestion = await _questionService.Update(question);
                return Ok(updatedQuestion);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (await _questionService.GetByID(id) != null)
            {
                await _questionService.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
