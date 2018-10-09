using Films.Data.SqlServerOdbc.Dto;
using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Films.Data.SqlServerOdbc
{
    public class SqlServerOdbcFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        SqlConnection connection;

        public SqlServerOdbcFilmDataGateway()
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
            producer.Connection = connection;
            producer.CommandText = $"insert into Producers(Name, Surname)values ('{film.Producer.Name}','{film.Producer.Surname}')";
            producer.ExecuteNonQuery();
            producer.CommandText = $"Select Producers.id from Producers where Producers.Name ='{film.Producer.Name}'and Producers.Surname = '{film.Producer.Surname}'";

            using (SqlDataReader readProducerId = producer.ExecuteReader())
            {
                while (readProducerId.Read())
                {
                    string stringProducerId = readProducerId["id"].ToString();
                    producerId = int.Parse(stringProducerId);
                }
            }

            SqlCommand addFilm = new SqlCommand();
            addFilm.Connection = connection;
            int bluRaySupport = film.BluRaySupport ? 1 : 0;
            addFilm.CommandText = $"Insert into Films (Name, Language, ReleaseDate, BluRaySupport, idProducer)Values('{film.Name}', '{film.Language}', '{film.ReleaseDate.ToString()}', {bluRaySupport}, {producerId})";
            addFilm.ExecuteNonQuery();
            addFilm.CommandText = $"select Films.id from Films Where Films.Name='{film.Name}' and Films.idProducer={producerId} and Films.ReleaseDate ='{film.ReleaseDate}'";

            using (SqlDataReader readFilmId = addFilm.ExecuteReader())
            {
                while (readFilmId.Read())
                {
                    string stringFilmId = readFilmId["id"].ToString();
                    filmId = int.Parse(stringFilmId);
                }
            }

            SqlCommand addActors = new SqlCommand();
            addActors.Connection = connection;

            foreach (Actor actor in film.Actors)
            {
                addActors.CommandText = $"insert into Actors (Name, Surname, idFilm)values('{actor.Name}', '{actor.Surname}', {filmId})";
                addActors.ExecuteNonQuery();
            }
            return true;
        }

        private Film CreateFilm(int filmId)
        {
            ICollection<Actor> actors = new List<Actor>();
            FilmDto filmDto = new FilmDto();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = $"select Films.name, Films.BluRaySupport, Films.ReleaseDate, Films.[Language], films.idProducer from Films where id ={filmId}";

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    filmDto.Name = (string)dataReader["name"];
                    filmDto.BluRaySupport = (bool)dataReader["BluRaySupport"];
                    filmDto.ReleaseDate = DateTime.Parse(dataReader["releaseDate"].ToString());
                    filmDto.Language = (string)dataReader["language"];
                    filmDto.ProducerId = (int)dataReader["idProducer"];
                }
            }

            SqlCommand getActors = new SqlCommand();
            getActors.Connection = connection;
            getActors.CommandText = "select name, surname, idFilm from actors";

            using (SqlDataReader dataReader = getActors.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    if (filmId == (int)dataReader["idFilm"])
                    {
                        Actor actor = new Actor((string)dataReader["Name"], (string)dataReader["Surname"]);
                        actors.Add(actor);
                    }
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

            return new Film(filmId, filmDto.BluRaySupport, filmDto.Name, filmDto.Language, producer, filmDto.ReleaseDate, actors);
        }

        protected override void Dispose(bool disposing)
        {
            connection.Close();
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
    }
}


//command.CommandText = $"select Films.name, Films.ReleaseDate, Films.[Language], films.idProducer, Actors.Name as actorName, Actors.Surname as actorSurname from Films inner join Actors on Actors.idFilm =Films.id where Actors.idFilm ={filmId}";
//command.Connection = connection;

//using (OdbcDataReader dataReader = command.ExecuteReader())
//{
//    while (dataReader.Read())
//    {
//        if (filmDto.Name == null)
//        {
//            filmDto.Name = (string)dataReader["name"];
//            filmDto.ReleaseDate = DateTime.Parse((string)dataReader["releaseDate"]);
//            filmDto.Language = (string)dataReader["language"];
//            filmDto.ProducerId = (int)dataReader["idProducer"];
//        }

//        Actor actor = new Actor((string)dataReader["actorName"], (string)dataReader["actorSurname"]);
//        actors.Add(actor);
//    }
//}