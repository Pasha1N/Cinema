using Films.Domain.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Films.Data.EntityFramework
{
    public class SqlServerEntityFrameworkFilmDataGateway: DisposableObject, IFilmDataGateway
    {
        IDbConnection connection;

        public SqlServerEntityFrameworkFilmDataGateway()
        {
          
        }

        public bool AddFilm(Film film)
        {
            return true;
        }

        public IEnumerable<Film> GetFilms()
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {


            }



                ICollection<Film> films = new List<Film>();

            return films;
        }


        protected override void Dispose(bool foo)
        {

        }
    }
}