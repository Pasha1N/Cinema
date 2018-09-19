using Movie.Domain.Models;
using MovieDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Presentation.Wpf.View.Model
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




    }
}