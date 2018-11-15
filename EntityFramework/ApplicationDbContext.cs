using Films.Data.EntityFramework.Models;
using System.Data.Entity;

namespace Films.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=StringConnectionEntityFrameworkSqlServer")
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Film> Films { get; set; }
    }
}