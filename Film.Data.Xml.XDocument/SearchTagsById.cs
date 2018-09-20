using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Film.Data.XmlXDocument
{
    internal class SearchTagsById
    {
        private XDocument document;

        public SearchTagsById(XDocument document)
        {
            this.document = document;
        }

        public int MaxMovieId()
        {
            int id = 1;

            IEnumerable<XElement> films = document.Root.Element("Films").Elements("film");

            foreach (XElement film in films)
            {
                if (id < int.Parse(film.Attribute("id").Value))
                {
                    id += int.Parse(film.Attribute("id").Value);
                }
            }
            return id;
        }

        public int MaxActorId()
        {
            int id = 1;

            IEnumerable<XElement> actors = document.Root.Element("Actors").Elements("Actor");

            foreach (XElement actor in actors)
            {
                if (id < int.Parse(actor.Attribute("id").Value))
                {
                    id += int.Parse(actor.Attribute("id").Value);
                }
            }
            return id;//Producers
        }

        public int MaxProducerId()
        {
            int id = 1;

            IEnumerable<XElement> producers = document.Root.Element("Producers").Elements("Producer");

            foreach (XElement producer in producers)
            {
                if (id < int.Parse(producer.Attribute("id").Value))
                {
                    id += int.Parse(producer.Attribute("id").Value);
                }
            }
            return id;
        }

        public int MaxLanguageId()
        {
            int id = 1;

            IEnumerable<XElement> languages = document.Root.Element("Languages").Elements("Language");

            foreach (XElement language in languages)
            {
                if (id < int.Parse(language.Attribute("id").Value))
                {
                    id += int.Parse(language.Attribute("id").Value);
                }
            }
            return id;
        }





    }
}