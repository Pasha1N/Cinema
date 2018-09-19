using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Film.Data.XmlXDocument
{
    public class XDocumentFilmDataGateway : IFilmDataGateway
    {
        private XDocument document;

        public Movie.Domain.Models.Film CreateFilm(Producer producer,)
        {
            return new Movie.Domain.Models.Film();
        }

        public IEnumerable<Movie.Domain.Models.Film> GetFilms()
        {
            IEnumerable<XElement> actors = document.Root.Element("")


        }


           






    }
}