using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Data.EntityFramework
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(): base("name=StringConnectionEntityFrameworkSqlExperss")
        {
        }

      public  DbSet<Film> Film { get;set; }
    }
}