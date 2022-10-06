using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProjectService : IGenericService<Project>
    {
        Task<Project> AddProject(int[] catIds, Project project);
    }
}
