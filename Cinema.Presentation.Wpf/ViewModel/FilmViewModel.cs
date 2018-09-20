using Movie.Domain.Models;
using MovieDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class FilmViewModel
    {
        Movie.Domain.Models.Film film;
        IFilmManager filmManager;

        public FilmViewModel(Movie.Domain.Models.Film film, FilmManager filmManager)
        {
            this.film = film;
            this.filmManager = filmManager;
        }

        public string Name => film.Name;
        public string Language => film.Language;
        public DateTime ReleaseDate => film.ReleaseDate;
        public int Id => film.Id;
    }
}