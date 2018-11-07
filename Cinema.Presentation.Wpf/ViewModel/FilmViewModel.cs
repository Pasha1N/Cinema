using Films.Domain;
using Films.Domain.Models;
using System.Collections.Generic;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class FilmViewModel
    {
        private Film film;
        private IFilmManager filmManager;

        public FilmViewModel(Film film, FilmManager filmManager)
        {
            this.film = film;
            this.filmManager = filmManager;
        }

        public IEnumerable<Actor> Actors => film.Actors;
        public string BluRaySupport => film.BluRaySupport == true ? "Yes" : "No";
        public int Id => film.Id;
        public string Language => film.Language;
        public string Name => film.Name;
        public Producer Producer => film.Producer;
        public string ReleaseDate => film.ReleaseDate.ToString();
    }
}