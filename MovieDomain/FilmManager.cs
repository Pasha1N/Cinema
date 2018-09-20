using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Film.Data;

namespace MovieDomain
{
    public class FilmManager : IFilmManager
    {
        private IFilmDataService filmDataService;

        public FilmManager(IFilmDataService filmDataService)
        {
            this.filmDataService = filmDataService;
        }

        public event EventHandler<FilmEventArgs> FilmAdded;

        public void AddFilm(Movie.Domain.Models.Film film)
        {
            bool added = false;

            using (IFilmDataGateway filmDataGateway = filmDataService.OpenDataGateway())
            {
                filmDataGateway.AddFilm(film);
            }

            if(added)
            {
                OnFilmAdded(new FilmEventArgs(film));
            }
        }

        public IEnumerable<Movie.Domain.Models.Film> GetFilms()
        {
            IFilmDataGateway dataGeteway = filmDataService.OpenDataGateway();

            return dataGeteway.GetFilms();
        }

        private void OnFilmAdded(FilmEventArgs e)
        {
            FilmAdded?.Invoke(this, e);
        }
    }
}