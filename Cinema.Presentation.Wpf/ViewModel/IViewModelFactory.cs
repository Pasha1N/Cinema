using Films.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public interface IViewModelFactory
    {
        FilmViewModel CreateFilmViewModel(Film film);
    }
}