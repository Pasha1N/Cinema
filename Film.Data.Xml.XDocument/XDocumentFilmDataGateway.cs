using Film.Data;
using Film.Data.XmlXDocument;
using Films;
using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Film.DataXmlXDocument
{
    public class XDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private XDocument document;
        private string path = @"../../../Films.xml";
        private SearchTagsById searchTagsById;

        public XDocumentFilmDataGateway()
        {
            document = XDocument.Load(path);
            searchTagsById = new SearchTagsById(document);
        }

        public bool AddFilm(Movie.Domain.Models.Film film)
        {
            int idFilm = searchTagsById.MaxMovieId();
            int idActor = searchTagsById.MaxActorId();
            int idProducer = searchTagsById.MaxProducerId();
            int idLanguage = searchTagsById.MaxLanguageId();

            IEnumerable<Actor> actors = film.Actors;
            ICollection<int> idActors = new List<int>();

            idLanguage += 1;
            document.Root.Element("Languages").Add(
                new XElement("Language")
                , new XAttribute("Name", film.Language)
                , new XAttribute("id", idLanguage));

            foreach (Actor actor in actors)
            {
                document.Root
                    .Element("Actors")
                    .Add(new XElement("Actor")
                    , new XAttribute("id", idActor)
                    , new XAttribute("Name", actor.Name)
                   , new XAttribute("Surname", actor.Surname));
                idActors.Add(idActor);
                idActor += 1;
            }

            XElement filmIsElement = new XElement(("Film")
                , new XAttribute("id", idFilm)
                , new XAttribute("Name", film.Name)
                , new XAttribute("Language", idLanguage)
                , new XAttribute("Producer", idProducer)
                );

            foreach (int id in idActors)
            {
                filmIsElement.Add(new XElement("Actor", id));
            }

            document.Root
               .Element("Films")
               .Add(filmIsElement);


            return true;
        }

       

        public Movie.Domain.Models.Film CreateFilm(XElement film)
          {
            IEnumerable<XElement> actorsIsElementsActors = film.Elements("Actor");
            ICollection<Actor> actors = new List<Actor>();
            string name =  film.Attribute("Name").Value;
            string language = FindLanguageById(int.Parse(film.Attribute("Language").Value));
            DateTime releaseDate = DateTime.Parse(film.Attribute("ReleaseDate").Value);
            Producer producer = FindProducerById(int.Parse(film.Attribute("Producer").Value));
            int id = int.Parse(film.Attribute("id").Value);

            foreach(XElement isActor in actorsIsElementsActors)
            {
                actors.Add(FindActorById(int.Parse(isActor.Value)));
            }

            return new Movie.Domain.Models.Film(id, name, language, producer,releaseDate,actors);
         }

        protected override void Dispose(bool disposing)
        {
            document.Save(path);
        }

        private Actor FindActorById(int id)
        {
            IEnumerable<XElement> actors = document.Root.Element("Actors").Elements("Actor");

            foreach (XElement actor in actors)
            {
                XAttribute attributeId = actor.Attribute("id");

                if(id.Equals(int.Parse(attributeId.Value)))
                {
                    Actor actor_ = new Actor(actor.Attribute("Name").Value, actor.Attribute("Surname").Value);
                    return actor_;
                }
            }
            throw new ArgumentException("Invalid Actor Id");
        }

        private Producer FindProducerById(int id)
        {
            IEnumerable<XElement> producers = document.Root.Element("Producers").Elements("Producer");

            foreach (XElement producer in producers)
            {
                XAttribute attributeId = producer.Attribute("id");

                if (id.Equals(int.Parse(attributeId.Value)))
                {
                   Producer producer_ = new Producer(producer.Attribute("Name").Value, producer.Attribute("Surname").Value);
                    return producer_;
                }
            }
            throw new ArgumentException("Invalid Producer Id");
        }

        private string FindLanguageById(int id)
        {
            IEnumerable<XElement> languages = document.Root.Element("Languages").Elements("Language");

            foreach (XElement language in languages)
            {
                XAttribute attributeId = language.Attribute("id");

                if (id.Equals(int.Parse(attributeId.Value)))
                {
                    return language.Attribute("Name").Value;
                }
            }
            throw new ArgumentException("Invalid Producer Id");
        }

        public IEnumerable<Movie.Domain.Models.Film> GetFilms()
        {
            IEnumerable<XElement> filmsIsElements = document.Root.Element("Films").Elements("Film");
            ICollection<Movie.Domain.Models.Film> films = new List<Movie.Domain.Models.Film>();

            foreach(XElement film in filmsIsElements)
            {
                films.Add(CreateFilm(film));
            }
            return films;
        }
    }
}