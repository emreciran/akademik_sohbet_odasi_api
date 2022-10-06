using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext db;

        public ProjectRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Project> AddProject(int[] catIds, Project project)
        {
            foreach (var catId in catIds)
            {
                var cat = db.Categories.Where(x => x.Category_ID == catId).FirstOrDefault();

                var projectCategory = new ProjectCategory()
                {
                    Project = project,
                    Category = cat
                };

                db.ProjectCategories.Add(projectCategory);
            }
            project.CreatedDate = DateTime.Now;

            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return project;
        }

        public async Task Delete(int id)
        {
            var deletedProject = await GetByID(id);
            db.Projects.Remove(deletedProject);
            await db.SaveChangesAsync();
        }

        public async Task<Project> GetByID(int id)
        {
            return await db.Projects.FindAsync(id);
        }

        public async Task<List<Project>> GetListAll()
        {
            return await db.Projects.ToListAsync();
        }

        public async Task<Project> Insert(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> Update(Project project)
        {
            db.Projects.Update(project);
            await db.SaveChangesAsync();
            return project;
        }
    }
}
