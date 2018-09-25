using Cinema.Presentation.Wpf.View;
using Films.Domain;
using Films.Domain.Models;
using Films.Wpf.Commands;
using Films.Wpf.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class MainViewModel : EventINotifyPropertyChanged
    {
        private ICommand addCommand;
        private ICollection<ActorViewModel> actorViewModels = new ObservableCollection<ActorViewModel>();
        private Film film;
        private IFilmManager filmManager;
        private ICollection<FilmViewModel> filmViewModels = new ObservableCollection<FilmViewModel>();
        private ProducerViewModel producerViewModels;
        private IViewModelFactory viewModelFactory;
        private ViewModelMakingFilm viewModelMakingFilm;
        private FilmViewModel selectedFilm;

        public MainViewModel(IFilmManager filmManager, IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            this.filmManager = filmManager;
            addCommand = new DelegateCommand(AddFilm);
            filmManager.FilmAdded += FilmManager_FilmAdd;

            foreach (Film film in filmManager.GetFilms())
            {
                FilmViewModel filmViewModel = (viewModelFactory.CreateFilmViewModel(film));
                filmViewModels.Add(filmViewModel);
            }
        }

        public ICommand AddCommand => addCommand;

        public IEnumerable<ActorViewModel> ActorViewModels => actorViewModels;

        public IEnumerable<FilmViewModel> FilmViewModels => filmViewModels;

        public ProducerViewModel ProducerViewModel => producerViewModels;

        public FilmViewModel SelectedFilm
        {
            get => selectedFilm;
            set => SetProperty(ref selectedFilm, value);
        }

        public void AddFilm()
        {
            Film newFilm = CreateFilm();

            if (newFilm != null)
            {
                filmManager.AddFilm(newFilm);
            }
        }

        public Film CreateFilm()
        {
            viewModelMakingFilm = new ViewModelMakingFilm();
            MakingFilm windowGetDataAboutFilm = new MakingFilm(viewModelMakingFilm);
            windowGetDataAboutFilm.ShowDialog();
            film = viewModelMakingFilm.GetFilm;

            if (film != null)
            {
                filmViewModels.Add(viewModelFactory.CreateFilmViewModel(film));
            }
            return film;
        }

        public void FilmManager_FilmAdd(object sender, FilmEventArgs e)
        {
            filmViewModels.Add(viewModelFactory.CreateFilmViewModel(e.Film));
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (selectedFilm != null)
            {
                actorViewModels.Clear();

                foreach (Actor actor in selectedFilm.Actors)
                {
                    actorViewModels.Add(new ActorViewModel(actor));
                }

                producerViewModels = new ProducerViewModel(selectedFilm.Producer);
            }
        }
    }
}