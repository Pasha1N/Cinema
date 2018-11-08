using Films.Data.EntityFramework.Models;
using System.Data.Entity;

namespace Films.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=StringConnectionEntityFrameworkSqlExperss")
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Film> Films { get; set; }
    }
}