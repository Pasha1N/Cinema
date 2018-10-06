using System;

namespace Films.Data.SqlServerOdbc.Dto
{
    public class FilmDto
    {
        public string Language { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}