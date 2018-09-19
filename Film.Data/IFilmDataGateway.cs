using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Data
{
    public interface IFilmDataGateway
    {
        bool AddFilm(string name);
        IEnumerable<Movie.Domain.Models.Film> GetFilms();
    }
}
