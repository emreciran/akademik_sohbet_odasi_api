using akademik_sohbet_odasi_api.Dto;
using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetListAll());
            return Ok(categories); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = _mapper.Map<CategoryDto>(await _categoryService.GetByID(id));
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound(); //404
        }

        [HttpGet]
        [Route("project/{projectID}")]
        public async Task<IActionResult> GetCategoryByProject(int projectID)
        {
            var categories = _mapper.Map<List<TagDto>>(await _categoryService.GetCategoryByProject(projectID));
            if (categories != null)
            {
                return Ok(categories);
            }
            return NotFound(); //404
        }

        [HttpGet("GetCategoryByName")]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string categoryName)
        {
            var categories = await _categoryService.GetCategoryByName(categoryName);
            if (categories != null)
            {
                return Ok(categories);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            var createdCategory = await _categoryService.Insert(category);
            return CreatedAtAction("GetAllCategory", new { id = createdCategory.Category_ID }, createdCategory);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (_categoryService.GetByID(category.Category_ID) != null)
            {
                var updatedCategory = await _categoryService.Update(category);
                return Ok(updatedCategory);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (await _categoryService.GetByID(id) != null)
            {
                await _categoryService.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
