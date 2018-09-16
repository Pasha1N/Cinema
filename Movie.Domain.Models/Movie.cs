using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Models
{
    public class Movie
    {
        private IEnumerable<Actor> actors;
        private Producer producer;
        private ReleaseDate releaseDate;

        public Movie(Producer producer, ReleaseDate releaseDate, IEnumerable<Actor> actors)
        {
            this.producer = producer;
            this.releaseDate = releaseDate;
            this.actors = actors;
        }

        public IEnumerable<Actor> Actors => actors;
        public Producer Producer => producer;
        public ReleaseDate ReleaseDate => releaseDate;
    }
}