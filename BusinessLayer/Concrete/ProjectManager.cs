using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProjectManager : IProjectService
    {
        IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> AddProject(int[] catIds, Project project)
        {
            return await _projectRepository.AddProject(catIds, project);
        }

        public async Task Delete(int id)
        {
            await _projectRepository.Delete(id);
        }

        public async Task<Project> GetByID(int id)
        {
            return await _projectRepository.GetByID(id);
        }

        public async Task<List<Project>> GetListAll()
        {
            return await _projectRepository.GetListAll();
        }

        public Task<Project> Insert(Project t)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> Update(Project project)
        {
            return await _projectRepository.Update(project);
        }
    }
}
