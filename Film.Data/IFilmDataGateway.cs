using Films.Domain.Models;
using System;
using System.Collections.Generic;

namespace Films.Data
{
    public interface IFilmDataGateway : IDisposable
    {
        bool AddFilm(Film film);
        IEnumerable<Film> GetFilms();
    }
}