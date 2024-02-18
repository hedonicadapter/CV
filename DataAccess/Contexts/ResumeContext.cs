using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ResumeContext : DbContext
    {
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {
        }
    }
}