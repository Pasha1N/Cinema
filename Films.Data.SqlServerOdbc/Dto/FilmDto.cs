using Films.Domain.Models;
using System;
using System.Collections.Generic;

namespace Films.Data.SqlServerOdbc.Dto
{
    public class FilmDto
    {
        private IEnumerable<Actor> Actors { get; set; }
        private int Id { get; set; }
        private string Language { get; set; }
        private string Name { get; set; }
        private Producer Producer { get; set; }
        private DateTime ReleaseDate { get; set; }
    }
}