using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public interface IViewModelFactory
    {
        FilmViewModel CreateFilmViewModel(Movie.Domain.Models.Film film);
    }
}