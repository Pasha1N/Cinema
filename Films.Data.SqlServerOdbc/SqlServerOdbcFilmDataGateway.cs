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
        private readonly OdbcConnection connection = new OdbcConnection("Driver={Sql Server};Server=DESKTOP-3C2NDOK;Database=FilmLibrary;Trusted_Connection=True");

        public SqlServerOdbcFilmDataGateway()
        {
            connection.Open();
        }

        public bool AddFilm(Film film)
        {
            int producerId = 0;
            OdbcCommand getProducerId = new OdbcCommand();



            getProducerId.CommandText = $"Select {producerId}= Producers.id" +
                $" for Producers " +
                $"where Producers.Name ={film.Producer.Name} and Producers.Surname = {film.Producer.Surname}"; 

     // OdbcCommand odbcCommand=new OdbcCommand($"Insert into FilmLibrary (Name, Language, ReleaseDate,ProducerId)Values" +
     //     $"()")


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