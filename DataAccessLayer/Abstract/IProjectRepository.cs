using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project> AddProject(int[] catIds, Project project);
    }
}
