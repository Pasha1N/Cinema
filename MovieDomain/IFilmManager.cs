using Films.Domain.Models;
using System;
using System.Collections.Generic;

namespace Films.Domain
{
    public interface IFilmManager
    {
        event EventHandler<FilmEventArgs> FilmAdded;

        void AddFilm(Film film);
        IEnumerable<Film> GetFilms();
    }
}