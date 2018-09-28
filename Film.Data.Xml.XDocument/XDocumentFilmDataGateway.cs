using Films.Domain.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Films.Data.Xml.xDocument
{
    public class XDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly XDocument document;
        private const string path = @"../../../Films.xml";
        private readonly SearchMaxId searchTagsById;
        private readonly ReturnIdOfElements returnIdOfElements;

        public XDocumentFilmDataGateway()
        {
            document = XDocument.Load(path);
            searchTagsById = new SearchMaxId(document);
            returnIdOfElements = new ReturnIdOfElements(document);
        }

        public bool AddFilm(Film film)
        {
            int idFilm = searchTagsById.MaxMovieId();
            int idActor = searchTagsById.MaxActorId();
            int idProducer = searchTagsById.MaxProducerId();
            int idLanguage = searchTagsById.MaxLanguageId();

            IEnumerable<Actor> actors = film.Actors;
            ICollection<int> idActors = new List<int>();

            if (returnIdOfElements.GetLanguageId(film.Language) == 0)
            {
                document.Root.Element("Languages").Add(
                     new XElement("Language"
                    , new XAttribute("id", idLanguage)
                    , new XAttribute("Name", film.Language)));
            }
            else
            {
                idLanguage = returnIdOfElements.GetLanguageId(film.Language);
            }

            foreach (Actor actor in actors)
            {
                if (returnIdOfElements.GetActorId(actor.Name, actor.Surname) == 0)
                {
                    idActor += 1;
                    document.Root
                        .Element("Actors")
                        .Add(new XElement("Actor"
                        , new XAttribute("id", idActor)
                        , new XAttribute("Name", actor.Name)
                       , new XAttribute("Surname", actor.Surname)));
                    idActors.Add(idActor);
                }
                else
                {
                    idActors.Add(returnIdOfElements.GetActorId(actor.Name, actor.Surname));
                }
            }

            if (returnIdOfElements.GetProducerId(film.Producer.Name, film.Producer.Surname) == 0)
            {
                idProducer += 1;
                document.Root.Element("Producers").Add(
                    new XElement("Producer"
                    , new XAttribute("id", idProducer)
                    , new XAttribute("Name", film.Producer.Name)
                    , new XAttribute("Surname", film.Producer.Surname))
                    );
            }

            idFilm += 1;
            XElement filmIsElement = new XElement(("Film")
                , new XAttribute("id", idFilm)
                , new XAttribute("Name", film.Name)
                , new XAttribute("Language", idLanguage)
                , new XAttribute("ReleaseDate", film.ReleaseDate.ToShortDateString())
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

        public Film CreateFilm(XElement film)
        {
            IEnumerable<XElement> actorsIsElementsActors = film.Elements("Actor");
            ICollection<Actor> actors = new List<Actor>();
            string name = film.Attribute("Name").Value;
            string language = FindLanguageById(int.Parse(film.Attribute("Language").Value));
            DateTime releaseDate = DateTime.Parse(film.Attribute("ReleaseDate").Value);
            Producer producer = FindProducerById(int.Parse(film.Attribute("Producer").Value));
            int id = int.Parse(film.Attribute("id").Value);

            foreach (XElement isActor in actorsIsElementsActors)
            {
                actors.Add(FindActorById(int.Parse(isActor.Value)));
            }

            return new Film(id, name, language, producer, releaseDate, actors);
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

                if (id.Equals(int.Parse(attributeId.Value)))
                {
                    Actor actor_ = new Actor(actor.Attribute("Name").Value, actor.Attribute("Surname").Value);
                    return actor_;
                }
            }
            throw new ArgumentException("Invalid Actor Id");
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

        public IEnumerable<Film> GetFilms()
        {
            IEnumerable<XElement> filmsIsElements = document.Root.Element("Films").Elements("Film");
            ICollection<Film> films = new List<Film>();

            foreach (XElement film in filmsIsElements)
            {
                films.Add(CreateFilm(film));
            }
            return films;
        }
    }
}