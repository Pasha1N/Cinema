using System;
using System.Collections.Generic;

namespace Films.Domain.Models
{
    public class Film
    {
        private IEnumerable<Actor> actors;
        private int id;
        private string language = string.Empty;
        private string name = string.Empty;
        private Producer producer;
        private DateTime releaseDate;

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
        public int Id => id;
        public string Language => language;
        public string Name => name;
        public Producer Producer => producer;
        public DateTime ReleaseDate => releaseDate;
    }
}