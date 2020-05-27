using Curriculum.Entities;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Server.Data
{
    public class CurriculumContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CurriculumDb;Username=postgres;Password=1234");
        }
    }
}
