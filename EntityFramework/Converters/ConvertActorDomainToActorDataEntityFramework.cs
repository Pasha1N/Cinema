namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertActorDomainToActorDataEntityFramework
    {
        public static EntityFramework.Models.Actor ConvertActor(Films.Domain.Models.Actor actorDomain)
        {
            EntityFramework.Models.Actor actor = new Models.Actor();

            actor.Name = actorDomain.Name;
            actor.Surname = actorDomain.Surname;

            return actor;
        }
    }
}