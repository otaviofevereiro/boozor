using Curriculum.Entities;
using DevPack.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Curriculum.Server.Data
{
    public class CurriculumContext : DevPackDbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Experience> Experiences { get; set; }


        public CurriculumContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
