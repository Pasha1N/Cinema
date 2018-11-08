using Films.Data.EntityFramework.Converters;
using Films.Domain.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;

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

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IEnumerable<Models.Film> films1 = context.Films.Include(film=>film.Actors).ToList();

                foreach (Models.Film film in films1)
                {
                    films.Add(ConvertFilm.ToModelsFilm(film));
                }
            }
            return films;
        }

        protected override void Dispose(bool foo)
        {
        }
    }
}