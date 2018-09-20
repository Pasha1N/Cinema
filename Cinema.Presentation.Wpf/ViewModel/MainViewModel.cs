using Films.Wpf.Commands;
using Films.Wpf.ViewModel;
using Movie.Domain.Models;
using MovieDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class MainViewModel : EventINotifyPropertyChanged
    {
        private IFilmManager filmManager;
        private IViewModelFactory viewModelFactory;
        private ICollection<ProducerViewModel> producerViewModels = new List<ProducerViewModel>();
        private ICollection<ActorViewModel> actorViewModels = new List<ActorViewModel>();
        private ICollection<FilmViewModel> filmViewModels = new List<FilmViewModel>();
        private ICommand addCommand;

        public MainViewModel(IFilmManager filmManager, IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            this.filmManager = filmManager;

            addCommand = new DelegateCommand(AddFilm);

            foreach(Movie.Domain.Models.Film film in filmManager.GetFilms())
            {
                FilmViewModel filmViewModel = (viewModelFactory.CreateFilmViewModel(film));
                filmViewModels.Add(filmViewModel);
            }

            filmManager.FilmAdded += FilmManager_FilmAdd;
        }

        public IEnumerable<FilmViewModel> FilmViewModels => filmViewModels;

        public void FilmManager_FilmAdd(object sender, FilmEventArgs e)
        {
            filmViewModels.Add(viewModelFactory.CreateFilmViewModel(e.Film));
        }

        public void AddFilm()
        {
            filmManager.AddFilm(CreateFilm());
        }

        public Movie.Domain.Models.Film CreateFilm()
        {
            ICollection<Actor> actors = new List<Actor>();
            Producer producer = new Producer("asd", "dsfwe");
            DateTime releaseDate = new DateTime(2017,6,12);

            Movie.Domain.Models.Film film = new Movie.Domain.Models.Film(4, "XXX", "English", producer, releaseDate, actors);
            return film;
        }


    }
}