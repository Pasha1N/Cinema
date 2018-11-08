using Films.Data.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=StringConnectionEntityFrameworkSqlServer")
        {
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<Actor> Actors { get; set; }
    }
}