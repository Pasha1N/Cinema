using System.Collections.Generic;

namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertFilmDomainToFilmDataEntityFramework
    {
        public static EntityFramework.Models.Film ConvertFilm(Films.Domain.Models.Film filmDomain)
        {
            EntityFramework.Models.Film film = new Models.Film();
            ICollection<Models.Actor> actors = new List<Models.Actor>();

            film.Id = filmDomain.Id;
            film.Name = filmDomain.Name;
            film.ReleaseDate = filmDomain.ReleaseDate;
            film.Language = filmDomain.Language;
            film.Producer = ConvertProducerDomainToProducerDataEntityFramework.ConvertProducer(filmDomain.Producer);
            film.BluRaySupport = filmDomain.BluRaySupport;

            foreach (Films.Domain.Models.Actor item in filmDomain.Actors)
            {
                actors.Add(ConvertActorDomainToActorDataEntityFramework.ConvertActor(item));
            }

            film.Actors=actors;

            return film;
        }
    }
}