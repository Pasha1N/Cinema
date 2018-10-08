using System;
using System.Collections.Generic;
using Films.Data;
using Films.Domain.Models;

namespace Films.Domain
{
    public class FilmManager : IFilmManager
    {
        private IFilmDataService filmDataService;

        public FilmManager(IFilmDataService filmDataService)
        {
            this.filmDataService = filmDataService;
        }

        public event EventHandler<FilmEventArgs> FilmAdded;

        public void AddFilm(Film film)
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

        public IEnumerable<Film> GetFilms()
        {
            using (IFilmDataGateway dataGeteway = filmDataService.OpenDataGateway())
            {
                return dataGeteway.GetFilms();
            }
        }

        private void OnFilmAdded(FilmEventArgs e)
        {
            FilmAdded?.Invoke(this, e);
        }
    }
}