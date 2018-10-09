using Films.Data.SqlServerOdbc.Dto;
using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;

namespace Films.Data.SqlServerOdbc
{
    public class SqlServerOdbcFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        OdbcConnection connection;

        public SqlServerOdbcFilmDataGateway()
        {
            connection = new OdbcConnection(ConfigurationManager
                .ConnectionStrings["DefaultConnectionToSQLServer"]
                .ConnectionString
               );

            connection.Open();
        }

        public bool AddFilm(Film film)
        {
            int producerId = 0;
            int filmId = 0;
            OdbcCommand producer = new OdbcCommand();
            producer.Connection = connection;
            producer.CommandText = $"insert into Producers(Name, Surname)values ('{film.Producer.Name}','{film.Producer.Surname}')";
            producer.ExecuteNonQuery();
            producer.CommandText = $"Select Producers.id from Producers where Producers.Name ='{film.Producer.Name}'and Producers.Surname = '{film.Producer.Surname}'";

            using (OdbcDataReader readProducerId = producer.ExecuteReader())
            {
                while (readProducerId.Read())
                {
                    string stringProducerId = readProducerId["id"].ToString();
                    producerId = int.Parse(stringProducerId);
                }
            }

            OdbcCommand addFilm = new OdbcCommand();
            addFilm.Connection = connection;
            addFilm.CommandText = $"Insert into Films (Name, Language, ReleaseDate,idProducer)Values('{film.Name}','{film.Language}','{film.ReleaseDate.ToString()}',{producerId})";
            addFilm.ExecuteNonQuery();
            addFilm.CommandText = $"select Films.id from Films Where Films.Name='{film.Name}' and Films.idProducer={producerId} and Films.ReleaseDate ='{film.ReleaseDate}'";

            using (OdbcDataReader readFilmId = addFilm.ExecuteReader())
            {
                while (readFilmId.Read())
                {
                    string stringFilmId = readFilmId["id"].ToString();
                    filmId = int.Parse(stringFilmId);
                }
            }

            OdbcCommand addActors = new OdbcCommand();
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
            OdbcCommand command = new OdbcCommand();
            command.Connection = connection;

            command.CommandText = $"select Films.name, Films.ReleaseDate, Films.[Language], films.idProducer from Films where id ={filmId}";

            using (OdbcDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    filmDto.Name = (string)dataReader["name"];
                    filmDto.ReleaseDate = DateTime.Parse((string)dataReader["releaseDate"]);
                    filmDto.Language = (string)dataReader["language"];
                    filmDto.ProducerId = (int)dataReader["idProducer"];
                }
            }

            OdbcCommand getActors = new OdbcCommand();
            getActors.Connection = connection;
            getActors.CommandText = "select name, surname, idFilm from actors";

            using (OdbcDataReader dataReader = getActors.ExecuteReader())
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

            using (OdbcDataReader dataReader = command.ExecuteReader())
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

        public IEnumerable<Film> GetFilms()
        {
            ICollection<Film> films = new List<Film>();
            ICollection<int> idFilms = new List<int>();
            OdbcCommand getFils = new OdbcCommand();
            getFils.CommandText = "Select id from Films";
            getFils.Connection = connection;

            using (OdbcDataReader dataReader = getFils.ExecuteReader())
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