using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Data.SqlServerOdbc
{
    public class SqlServerOdbcFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly OdbcConnection connection = new OdbcConnection("Driver={SQL Server};Server=(local);Database=BookShop;Trusted_Security=Yes;");

        public SqlServerOdbcFilmDataGateway()
        {
            connection.Open();
        }

        public bool AddFilm(Film film)
        {
            int producerId = 0;
            OdbcCommand getProducerId = new OdbcCommand();

          //  getProducerId.CommandText = $"Select {producerId}= ";


//OdbcCommand odbcCommand=new OdbcCommand($"Insert into FilmLibrary (Name, Language, ReleaseDate, )")


            return true;
        }

        public IEnumerable<Film> GetFilms()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            connection.Close();
        }
    }
}