using System.Collections.Generic;
using System.Xml.Linq;

namespace Films.Data.Xml.xDocument
{
    public class ReturnIdOfElements
    {
        private XDocument document;

        public ReturnIdOfElements(XDocument document)
        {
            this.document = document;
        }

        public int GetActorId(string name, string surname)
        {
            IEnumerable<XElement> actors = document.Root.Element("Actors").Elements("Actor");

            foreach (XElement actor in actors)
            {
                if (name.Equals(actor.Attribute("Name").Value) && surname.Equals(actor.Attribute("Surname").Value))
                {
                    return int.Parse(actor.Attribute("id").Value);
                }
            }
            return 0;
        }

        public int GetLanguageId(string name)
        {
            IEnumerable<XElement> languages = document.Root.Element("Languages").Elements("Language");

            foreach (XElement language in languages)
            {
                if (name.Equals(language.Attribute("Name").Value))
                {
                    return int.Parse(language.Attribute("id").Value);
                }
            }
            return 0;
        }

        public int GetProducerId(string name, string surname)
        {
            IEnumerable<XElement> producers = document.Root.Element("Producers").Elements("Producer");

            foreach (XElement producer in producers)
            {
                if (name.Equals(producer.Attribute("Name").Value) && surname.Equals(producer.Attribute("Surname").Value))
                {
                    return int.Parse(producer.Attribute("id").Value);
                }
            }
            return 0;
        }
    }
}