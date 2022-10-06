using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
    public class ProjectsController : ControllerBase
    {
        IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            var projects = await _projectService.GetListAll();
            return Ok(projects); // 200 + data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetByID(id);
            if (project != null)
            {
                return Ok(project);
            }
            return NotFound(); //404
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromQuery] int[] catIds, [FromBody] Project project)
        {
            var createdProject = await _projectService.AddProject(catIds, project);
            return CreatedAtAction("GetAllProject", new { id = createdProject.Project_ID }, createdProject);//201 + data
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            if (_projectService.GetByID(project.Project_ID) != null)
            {
                var updatedProject = await _projectService.Update(project);
                return Ok(updatedProject);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (await _projectService.GetByID(id) != null)
            {
                await _projectService.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
