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
        public bool AddFilm(Film film)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Films.Add(ConvertFilm.ToEntityFrameworkModelsFilm(film));
                context.SaveChanges();
            }
                return true;
        }

        public IEnumerable<Film> GetFilms()
        {
            ICollection<Film> films = new List<Film>();
            ICollection<Models.Film> films1 = new List<Models.Film>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                // foreach(Models.Film film in context.Films)
                //  {
                //    films.Add(ConvertFilm.ToModelsFilm(film));
                // }
                films1 = context.Films;
            }

            return films;
        }

        protected override void Dispose(bool foo)
        {
        }
    }
}