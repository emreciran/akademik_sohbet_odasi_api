using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //QuestionTag
            modelBuilder.Entity<QuestionTag>()
                .HasKey(x => new { x.Question_ID, x.Tag_ID });
            modelBuilder.Entity<QuestionTag>()
                .HasOne(x => x.Question)
                .WithMany(x => x.QuestionTags)
                .HasForeignKey(x => x.Question_ID);
            modelBuilder.Entity<QuestionTag>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.QuestionTags)
                .HasForeignKey(x => x.Tag_ID);

            //UserProject
            modelBuilder.Entity<UserProject>()
                .HasKey(x => new { x.User_ID, x.Project_ID });
            modelBuilder.Entity<UserProject>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserProjects)
                .HasForeignKey(x => x.User_ID);
            modelBuilder.Entity<UserProject>()
                .HasOne(x => x.Project)
                .WithMany(x => x.UserProjects)
                .HasForeignKey(x => x.Project_ID);

            //ProjectCategory
            modelBuilder.Entity<ProjectCategory>()
                .HasKey(x => new { x.Project_ID, x.Category_ID });
            modelBuilder.Entity<ProjectCategory>()
                .HasOne(x => x.Project)
                .WithMany(x => x.ProjectCategories)
                .HasForeignKey(x => x.Project_ID);
            modelBuilder.Entity<ProjectCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ProjectCategories)
                .HasForeignKey(x => x.Category_ID);
        }
    }
}
