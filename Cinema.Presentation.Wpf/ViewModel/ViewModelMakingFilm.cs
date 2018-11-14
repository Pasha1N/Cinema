using Films.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Films.Wpf.Commands;
using System.Windows.Input;
using Films.Domain.Models;
using System.Windows.Controls;

namespace Cinema.Presentation.Wpf.ViewModel
{
    public class ViewModelMakingFilm : EventINotifyPropertyChanged
    {
        ICollection<Actor> actors = new ObservableCollection<Actor>();
        private string actorName = string.Empty;
        private string actorSurname = string.Empty;
        private bool bluRaySupport = false;
        private bool canAddActor = true;
        private bool canCreateFilm = false;
        private Command commandAddActor;
        private Command commandCreateFilm;
        private Film film;
        private bool toolTipIsEnable = false;
      //  private string toolTipMessage = "Date should start from ( 1895/1/1 )";
        private string language = string.Empty;
        private string name = string.Empty;
        private string producerName = string.Empty;
        private string producerSurname = string.Empty;
        //   private DateTime releaseDate = new DateTime(DatePicker.DisplayDateStartProperty(DateTime.Parse("05.10.2000")));
        DatePicker releaseDate = new DatePicker();
        private FilmViewModel selectedFilm;

        public ViewModelMakingFilm()
        {;
            ReleaseDate.DisplayDateStart = DateTime.Parse("05.10.2000");
            commandCreateFilm = new DelegateCommand(AssembleTheFilm, EnableFilmCreationButton);
            commandAddActor = new DelegateCommand(AddActor, EnableAddActorButton);
        }

        public IEnumerable<Actor> Actors => actors;

        public string ActorName
        {
            get => actorName;
            set => SetProperty(ref actorName, value);
        }

        public string ActorSurname
        {
            get => actorSurname;
            set => SetProperty(ref actorSurname, value);
        }

        public bool BluRaySupport
        {
            get => bluRaySupport;
            set => bluRaySupport = value;
        }

        public bool CanAddActor
        {
            get => canAddActor;
            set => SetProperty(ref canAddActor, value);
        }

        public bool CanCreateFilm
        {
            get => canCreateFilm;
            set => SetProperty(ref canCreateFilm, value);
        }

        public ICommand CommandAddActor => commandAddActor;

        public ICommand CommandCreateFilm => commandCreateFilm;

        public Film GetFilm => film;

        public string Language
        {
            set => SetProperty(ref language, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string ProducerName
        {
            set => SetProperty(ref producerName, value);
        }

        public string ProducerSurname
        {
            set => SetProperty(ref producerSurname, value);
        }

        public DatePicker ReleaseDate
        {
            get => releaseDate;


            set
             {
                if(releaseDate!=value)
                {
                    //DateTime date = new DateTime(1895, 1, 1);

                    //if(releaseDate>=date)
                    //{
                    //    ToolTipIsEnable = false;
                    //    CanCreateFilm = true;
                    //}
                    //else
                    //{
                    //    ToolTipIsEnable = true;
                    //    CanCreateFilm = false;
                    //}
                    //  string date = value.ToShortDateString();
                    // releaseDate = DateTime.Parse(date);

                    ToolTipIsEnable = true;
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ReleaseDate)));
                }
            }
        }

        public FilmViewModel SelectedFilm
        {
            get => selectedFilm;
            set => SetProperty(ref selectedFilm, value);
        }

        public bool ToolTipIsEnable
        {
            get
            {
                return toolTipIsEnable;
            }
            set => SetProperty(ref toolTipIsEnable, value);
        }

       // public string ToolTipMessage
        //{
          //  get => toolTipMessage;
           // set => SetProperty(ref toolTipMessage, value);
        //}

        public event EventHandler<EventArgs> ClickingButtonCreateFilm;

        public void AddActor()
        {
            actors.Add(CreateActor(actorName, actorSurname));
            actorName = string.Empty;
            actorSurname = string.Empty;
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ActorName)));
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ActorSurname)));

        }

        public void AssembleTheFilm()
        {
            int id = 0;
            film = new Film(
                id
                ,bluRaySupport
                , name
                , language
                , CreateProducer(producerName, producerSurname)
                , DateTime.Parse(releaseDate.ToString()), actors);

            CanCreateFilm = false;
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(CanCreateFilm)));

            ClickingButtonCreateFilm?.Invoke(this, EventArgs.Empty);
        }

        private Actor CreateActor(string name, string surname)
        {
            return new Actor(name, surname);
        }

        private Producer CreateProducer(string name, string surname)
        {
            return new Producer(name, surname);
        }

        public bool EnableAddActorButton()
        {
            return CanAddActor;
        }

        public bool EnableFilmCreationButton()
        {
            return CanCreateFilm;
        }

        //public string ToolTipMessage
        //{
        //    get => toolTipMessage;
        //    set => SetProperty(ref toolTipMessage, value);
        //}

        //public void UpdateFilmCreationStatus(int lengthTheString, int minimumStringLength, string errorMessage)
        //{
        //    if (lengthTheString < minimumStringLength)
        //    {
        //        CanCreateFilm = false;
        //        ToolTipMessage = errorMessage;
        //    }
        //    else
        //    {
        //        CanCreateFilm = true;
        //    }
        //}

        //public void UpdateStatusAddingActor(int lengthTheString, int minimumStringLength, string errorMessage)
        //{
        //    if (lengthTheString < minimumStringLength)
        //    {
        //        CanAddActor = false;
        //        ToolTipMessage = errorMessage;
        //    }
        //    else
        //    {
        //        CanAddActor = true;
        //    }
        //}
    }
}