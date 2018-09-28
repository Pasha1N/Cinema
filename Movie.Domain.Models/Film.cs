using System;
using System.Collections.Generic;

namespace Films.Domain.Models
{
    public class Film
    {
        private readonly IEnumerable<Actor> actors;
        private readonly int id;
        private readonly string language = string.Empty;
        private readonly string name = string.Empty;
        private readonly Producer producer;
        private readonly DateTime releaseDate;

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