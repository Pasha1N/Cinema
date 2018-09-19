using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Models
{
    public class Film
    {
        private IEnumerable<Actor> actors;
        private Producer producer;
        private DateTime releaseDate;
        private string name;
        private string language;

        public Film(Producer producer, DateTime releaseDate, IEnumerable<Actor> actors)
        {
            this.producer = producer;
            this.releaseDate = releaseDate;
            this.actors = actors;
        }

        public IEnumerable<Actor> Actors => actors;
        public Producer Producer => producer;
        public DateTime ReleaseDate => releaseDate;
        public string Name => name;
        public string Language => language;
    }
}