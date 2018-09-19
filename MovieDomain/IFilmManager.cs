using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain
{
    public interface IFilmManager
    {
        event EventHandler<FilmEventArgs> FilmAdded;

        IEnumerable<Movie.Domain.Models.Film> GetFilms();
        void AddFilm(Movie.Domain.Models.Film film);
    }
}