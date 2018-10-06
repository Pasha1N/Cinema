using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Films.Data.SqlServerOdbc.Dto;
using System.Configuration;

namespace Films.Data.SqlServerSqlClient
{
    public class SqlServerSqlClientFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        SqlConnection connection;

        public SqlServerSqlClientFilmDataGateway()
        {
            connection = new SqlConnection(ConfigurationManager
                .ConnectionStrings["DefaultConnectionToSQLExpress"]
                .ConnectionString
                );

            connection.Open();
        }

        public bool AddFilm(Film film)
        {
            int producerId = 0;
            int filmId = 0;
            SqlCommand producer = new SqlCommand();

            producer.CommandText = $"insert into Producers(Name, Surname)]values ({film.Producer.Name},{film.Producer.Surname})";

            producer.CommandText = $"Select Producers.id for Producers where Producers.Name ={film.Producer.Name} and Producers.Surname = {film.Producer.Surname}";

            using (SqlDataReader readProducerId = producer.ExecuteReader())
            {
                string stringProducerId = readProducerId["id"].ToString();
                producerId = int.Parse(stringProducerId);
            }

            SqlCommand addFilm = new SqlCommand();
            addFilm.CommandText = $"Insert into FilmLibrary (Name, Language, ReleaseDate,ProducerId)Values({film.Name},{film.Language},{film.ReleaseDate},{producerId})";

            addFilm.CommandText = $"select Films.id from Film Where Films.Name={film.Name} and Films.ProducerId={producerId} and Films.ReleaseDate ={film.ReleaseDate}";

            using (SqlDataReader readFilmId = addFilm.ExecuteReader())
            {
                string stringFilmId = readFilmId["id"].ToString();

                filmId = int.Parse(stringFilmId);
            }

            SqlCommand addActors = new SqlCommand();

            foreach (Actor actor in film.Actors)
            {
                addActors.CommandText = "insert into Actors (Name, Surname, idFilm)values({actor.Name}, {actor.Surname}, {filmId})";
            }

            return true;
        }

        public IEnumerable<Film> GetFilms()
        {
            ICollection<Film> films = new List<Film>();
            ICollection<int> idFilms = new List<int>();
            SqlCommand getFils = new SqlCommand();
            getFils.CommandText = "Select id from Films";
            getFils.Connection = connection;

            using (SqlDataReader dataReader = getFils.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    idFilms.Add(int.Parse(dataReader["id"].ToString()));
                }
            }

            foreach (int id in idFilms)
            {
                films.Add(CreateFilm(id));
            }

            return films;
        }

        private Film CreateFilm(int filmId)
        {
            ICollection<Actor> actors = new List<Actor>();
            FilmDto filmDto = new FilmDto();
            SqlCommand command = new SqlCommand();

            command.CommandText = $"select Films.name, Films.ReleaseDate, Films.[Language], films.idProducer, Actors.Name as actorName, Actors.Surname as actorSurname from Films inner join Actors on Actors.idFilm =Films.id where Actors.idFilm ={filmId}";
            command.Connection = connection;

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    if (filmDto.Name == null)
                    {
                        filmDto.Name = (string)dataReader["name"];
                        filmDto.ReleaseDate = (DateTime)dataReader["releaseDate"];
                        filmDto.Language = (string)dataReader["language"];
                        filmDto.ProducerId = (int)dataReader["idProducer"];
                    }

                    Actor actor = new Actor((string)dataReader["actorName"], (string)dataReader["actorSurname"]);
                    actors.Add(actor);
                }
            }

            command.CommandText = $"select name, surname from Producers where id={filmDto.ProducerId}";
            command.Connection = connection;
            Producer producer = null;

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    producer = new Producer((string)dataReader["name"], (string)dataReader["surname"]);
                }
            }

            return new Film(filmId, filmDto.Name, filmDto.Language, producer, filmDto.ReleaseDate, actors);
        }

        protected override void Dispose(bool disposing)
        {
            connection.Close();
        }
    }
}