using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Models;

namespace onlinelearningbackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUserModel,MyRoleModel,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<TaskClass> Tasks { get; set; }
        public virtual DbSet<TaskSolution> TaskSolutions { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<ProjectModel> ProjectModels { get; set; }
        public virtual DbSet<TextMaterial> TextMaterials { get; set; }
        public virtual DbSet<VideoMaterial> VideoMaterials { get; set; }
        public virtual DbSet<LinkMaterial> LinkMaterials { get; set; }
        public virtual DbSet<CourseMaterialModel> CourseMaterialModels { get; set; }
        public virtual DbSet<ProjectMaterialModel> ProjectMaterialModels { get; set; }
        public virtual DbSet<UserProjectModel> UserProjectModels { get; set; }
        public virtual DbSet<CourseMyUserModel> CourseMyUserModel { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }



    }
}
