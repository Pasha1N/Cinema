
using Films.Domain;
using Films.Domain.Models;
using System;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class FilmViewModel
    {
        Film film;
        IFilmManager filmManager;

        public FilmViewModel(Film film, FilmManager filmManager)
        {
            this.film = film;
            this.filmManager = filmManager;
        }

        public int Id => film.Id;
        public string Language => film.Language;
        public string Name => film.Name;
        public DateTime ReleaseDate => film.ReleaseDate;
    }
}