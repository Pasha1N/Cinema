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
        private readonly OdbcConnection connection = new OdbcConnection("Driver={Sql Server};Server=(local);Database=FilmLibrary;Trusted_Connection=True");

        public SqlServerOdbcFilmDataGateway()
        {
            connection.Open();
        }

        public bool AddFilm(Film film)
        {
            int producerId = 0;
            int filmId = 0;

            OdbcCommand producer= new OdbcCommand();

            producer.CommandText = $"insert into Producers(Name, Surname)]values " +
                $"({film.Producer.Name},{film.Producer.Surname})";

            producer.CommandText = $"Select Producers.id" +
                $" for Producers " +
                $"where Producers.Name ={film.Producer.Name} and Producers.Surname = {film.Producer.Surname}";

            using (OdbcDataReader readProducerId = producer.ExecuteReader())
            {
               string stringProducerId = readProducerId["id"].ToString();
                producerId = int.Parse(stringProducerId);
            }

            OdbcCommand addFilm = new OdbcCommand();
            addFilm.CommandText = $"Insert into FilmLibrary (Name, Language, ReleaseDate,ProducerId)Values" +
                $"({film.Name},{film.Language},{film.ReleaseDate},{producerId})";

            addFilm.CommandText = $"select Films.id from Film Where Films.Name={film.Name} " +
                $"and Films.ProducerId={producerId} and Films.ReleaseDate ={film.ReleaseDate}";

            using (OdbcDataReader readFilmId = addFilm.ExecuteReader())
            {
                string stringFilmId = readFilmId["id"].ToString();

                filmId = int.Parse(stringFilmId);
            }

            OdbcCommand addActors = new OdbcCommand();
            
            foreach( Actor actor in film.Actors)
            {
                addActors.CommandText = "insert into Actors (Name, Surname, idFilm)values" +
              $"({actor.Name}, {actor.Surname}, {filmId})";
            }
          

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