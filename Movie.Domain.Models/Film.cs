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
        private int id;

        public Film(int id, string name, string language, Producer producer, DateTime releaseDate, IEnumerable<Actor> actors)
        {
            this.producer = producer;
            this.releaseDate = releaseDate;
            this.actors = actors;
            this.name = name;
            this.language = language;
            this.id = id;
        }

        public IEnumerable<Actor> Actors => actors;
        public Producer Producer => producer;
        public DateTime ReleaseDate => releaseDate;
        public string Name => name;
        public string Language => language;
        public int Id => id;
    }
}