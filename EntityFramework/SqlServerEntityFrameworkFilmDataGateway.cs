using Films.Data.EntityFramework.Converters;
using Films.Domain.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Films.Data.EntityFramework
{
    public class SqlServerEntityFrameworkFilmDataGateway: DisposableObject, IFilmDataGateway
    {
        public SqlServerEntityFrameworkFilmDataGateway() 
        {
        }

        public bool AddFilm(Film film)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Films.Add(ConvertFilmDomainToFilmDataEntityFramework.ConvertFilm(film));
                context.SaveChanges();
            }
                return true;
        }

        public IEnumerable<Film> GetFilms()
        {

            //using (ApplicationDbContext dbContext = new ApplicationDbContext())
            //{


            //}



                ICollection<Film> films = new List<Film>();

            return films;
        }


        protected override void Dispose(bool foo)
        {

        }
    }
}