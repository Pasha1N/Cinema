using Movie.Domain.Models;
using System;

namespace MovieDomain
{
    public class FilmEventArgs : EventArgs
    {
        Movie.Domain.Models.Film film;

        public FilmEventArgs(Movie.Domain.Models.Film film)
        {
            this.film = film;
        }

        public Movie.Domain.Models.Film Film => film;
    }
}