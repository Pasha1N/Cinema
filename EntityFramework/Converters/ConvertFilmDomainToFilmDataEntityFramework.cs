using System.Collections.Generic;
using System.Linq;

namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertFilm
    {
        public static EntityFramework.Models.Film ToEntityFrameworkModelsFilm(Films.Domain.Models.Film filmDomain)
        {
            EntityFramework.Models.Film film = new Models.Film();
            ICollection<Models.Actor> actors = new List<Models.Actor>();

            film.Id = filmDomain.Id;
            film.Name = filmDomain.Name;
            film.ReleaseDate = filmDomain.ReleaseDate;
            film.Language = filmDomain.Language;
            film.Producer = ConvertProducer.ToEntityFrameworkModelsProducer(filmDomain.Producer);
            film.BluRaySupport = filmDomain.BluRaySupport;

            foreach (Films.Domain.Models.Actor item in filmDomain.Actors)
            {
                actors.Add(ConvertActor.ToEntityFrameworkModelsActor(item));
            }

            film.Actors = actors;

            return film;
        }

        public static Films.Domain.Models.Film ToModelsFilm(Models.Film otherFilm)
        {
            ICollection<Films.Domain.Models.Actor> actors = new List<Films.Domain.Models.Actor>();

            foreach (Models.Actor actor in otherFilm.Actors)
            {
                actors.Add(ConvertActor.ToModelsActor(actor));
            }

            return new Domain.Models.Film(
                 otherFilm.Id
                , otherFilm.BluRaySupport
                , otherFilm.Name
                , otherFilm.Language
                , ConvertProducer.ToModelsProducer(otherFilm.Producer)
                , otherFilm.ReleaseDate
                , actors
                );
        }
    }
}