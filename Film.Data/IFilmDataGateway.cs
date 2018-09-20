using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Data
{
    public interface IFilmDataGateway:IDisposable
    {
        bool AddFilm(Movie.Domain.Models.Film film);
        IEnumerable<Movie.Domain.Models.Film> GetFilms();
    }
}
