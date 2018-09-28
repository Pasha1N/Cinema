using Films.Domain.Models;
using System;

namespace Films.Domain
{
    public class FilmEventArgs : EventArgs
    {
        private Film film;

        public FilmEventArgs(Film film)
        {
            this.film = film;
        }

        public Film Film => film;
    }
}