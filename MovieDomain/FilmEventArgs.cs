using Movie.Domain.Models;
using System;

namespace MovieDomain
{
    public class FilmEventArgs : EventArgs
    {
        Film film;

        public FilmEventArgs(Film film)
        {
            this.film = film;
        }

        public Film Film => film;
    }
}