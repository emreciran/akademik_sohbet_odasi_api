using akademik_sohbet_odasi_api.Dto;
using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class TagsController : ControllerBase
    {
        ITagService _tagService;
        IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTag()
        {
            var tags = _mapper.Map<List<TagDto>>(await _tagService.GetListAll());
            return Ok(tags); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = _mapper.Map<TagDto>(await _tagService.GetByID(id));
            if (tag != null)
            {
                return Ok(tag);
            }
            return NotFound(); //404
        }

        [HttpGet]
        [Route("question/{questionId}")]
        public async Task<IActionResult> GetTagByQuestion(int questionId)
        {
            var tags = _mapper.Map<List<TagDto>>(await _tagService.GetTagByQuestion(questionId));
            if (tags != null)
            {
                return Ok(tags);
            }
            return NotFound(); //404
        }

        [HttpGet("GetTagByName")]
        public async Task<IActionResult> GetTagByName([FromQuery] string tagName)
        {
            var tags = await _tagService.GetTagByName(tagName);
            if (tags != null)
            {
                return Ok(tags);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(Tag tag)
        {
            var createdTag = await _tagService.Insert(tag);
            return CreatedAtAction("GetAllTag", new { id = createdTag.Tag_ID }, createdTag);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTag(Tag tag)
        {
            if (_tagService.GetByID(tag.Tag_ID) != null)
            {
                var updatedTag = await _tagService.Update(tag);
                return Ok(updatedTag);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (await _tagService.GetByID(id) != null)
            {
                await _tagService.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
