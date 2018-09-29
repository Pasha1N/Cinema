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

            OdbcCommand producer = new OdbcCommand();

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

            foreach (Actor actor in film.Actors)
            {
                addActors.CommandText = "insert into Actors (Name, Surname, idFilm)values" +
              $"({actor.Name}, {actor.Surname}, {filmId})";
            }

            return true;
        }

        public IEnumerable<Film> GetFilms()
        {
            ICollection<Film> films = new List<Film>();
            OdbcCommand getFils = new OdbcCommand();
            getFils.CommandText = "Select id from Films";

            using (OdbcDataReader dataReader = getFils.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Film film = CreateFilm(int.Parse(dataReader["id"].ToString()));
                    films.Add(film);
                }
            }
            return films;
        }

        private Film CreateFilm(int filmId)
        {
            Film film;
            OdbcCommand command = new OdbcCommand();
            command.CommandText = $"select  Name, Language, ReleaseDate from Films where Films.Id ={filmId}";

            using (OdbcDataReader dataRead = command.ExecuteReader())
            {
                film = new Film(
                    filmId
                    , dataRead["Name"].ToString()
                    , dataRead["Language"].ToString()
                    , GetProducer(filmId)
                    , DateTime.Parse(dataRead["ReleaseDate"].ToString())
                    , GetActors(filmId)
                    );
            }

            return film;
        }


        private IEnumerable<Actor> GetActors(int filmId)
        {
            ICollection<Actor> actors = new List<Actor>();

            OdbcCommand command = new OdbcCommand();
            command.CommandText = $"select Name, Surname from Actors where Actor.idFilm ={filmId}";

            using (OdbcDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    actors.Add(new Actor(dataReader["Name"].ToString(), dataReader["Surname"].ToString()));
                }
            }
            return actors;
        }

        private Producer GetProducer(int filmId)
        {
            int idProducer;
            OdbcCommand command = new OdbcCommand();
            Producer producer;
            command.CommandText = $"select id from Producers Where Film.id{filmId}";

            using (OdbcDataReader dataReader = command.ExecuteReader())
            {
                idProducer = int.Parse(dataReader["id"].ToString());
            }

            command.CommandText = $"select Name, Surname from Producers where Prducers.id={idProducer}";

            using (OdbcDataReader dataReader = command.ExecuteReader())
            {
                producer = new Producer(dataReader["Name"].ToString(), dataReader["Surname"].ToString());
            }

            return producer;
        }

        protected override void Dispose(bool disposing)
        {
            connection.Close();
        }
    }
}