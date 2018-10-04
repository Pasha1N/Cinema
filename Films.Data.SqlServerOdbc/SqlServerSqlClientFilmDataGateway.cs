using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Films.Data.SqlServerSqlClient
{
    public class SqlServerSqlClientFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        //private readonly SqlConnection connection = new SqlConnection("Provider={SQLNCLI};Server=(local);Database=FilmLibrary;Trusted_Connection=True");
        private readonly SqlConnection connection = new SqlConnection("Server=(local);Database=FilmLibrary;Trusted_Connection=yes;");
        public SqlServerSqlClientFilmDataGateway()
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

            foreach(int id in idFilms )
            {
                films.Add(CreateFilm(id));
            }

            return films;
        }

        private Film CreateFilm(int filmId)
        {
            Film film;
            SqlCommand command = new SqlCommand();
            command.CommandText = $"select  Name, Language, ReleaseDate from Films where Films.Id ={filmId}";
            command.Connection = connection;

            using (SqlDataReader dataRead = command.ExecuteReader())
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

            SqlCommand command = new SqlCommand();
            command.CommandText = $"select Name, Surname from Actors where Actor.idFilm ={filmId}";
            command.Connection = connection;

            using (SqlDataReader dataReader = command.ExecuteReader())
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
            SqlCommand command = new SqlCommand();
            Producer producer;
            command.CommandText = $"select id from Producers Where Film.id{filmId}";
            command.Connection = connection;

            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                idProducer = int.Parse(dataReader["id"].ToString());
            }

            command.CommandText = $"select Name, Surname from Producers where Prducers.id={idProducer}";

            using (SqlDataReader dataReader = command.ExecuteReader())
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


//private readonly SqlConnection connection = new SqlConnection("Provider=SQLNCLI;Server=myServerAddress;Database=myDataBase;Trusted_Connection=yes;");
//Говорит что ключевое слово Provider не поддерживается что делать?