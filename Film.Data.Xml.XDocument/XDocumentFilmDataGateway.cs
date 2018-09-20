using Films;
using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Film.Data.XmlXDocument
{
    public class XDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private XDocument document;

        public Movie.Domain.Models.Film CreateFilm(XElement film)
          {
            IEnumerable<XElement> actorsIsElementsActors = film.Elements("Actor");
            ICollection<Actor> actors = new List<Actor>();
            string name =  film.Attribute("Name").Value;
            string language = FindLanguageById(int.Parse(film.Attribute("Language").Value));
            DateTime releaseDate = DateTime.Parse(film.Attribute("Language").Value);
            Producer producer = FindProducerById(int.Parse(film.Attribute("Producer").Value));
           
            foreach(XElement isActor in actorsIsElementsActors)
            {
                actors.Add(FindActorById(int.Parse(isActor.Value)));
            }

            return new Movie.Domain.Models.Film(name, language, producer,releaseDate,actors);
         }

        //private Actor CreateActor(XElement actorIsElement)
        //{
        //  return new Actor(actorIsElement.Attribute("Name").Value, actorIsElement.Attribute("Surname").Value);
        //}

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
            IEnumerable<XElement> actors = document.Root.Element("")

        }


   
    }
}