using System;
using System.Collections.Generic;

namespace Films.Data.EntityFramework.Models
{
    public class Film
    {
        public ICollection<Actor> Actors { get; set; }

        public bool BluRaySupport { get; set; }

        public int Id { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }

        public Producer Producer { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}