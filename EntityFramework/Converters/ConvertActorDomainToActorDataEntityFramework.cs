namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertActor
    {
        public static EntityFramework.Models.Actor ToEntityFrameworkModelsActor(Films.Domain.Models.Actor actorDomain)
        {
            EntityFramework.Models.Actor actor = new Models.Actor();
            actor.Name = actorDomain.Name;
            actor.Surname = actorDomain.Surname;

            return actor;
        }

        public static Films.Domain.Models.Actor ToModelsActor(EntityFramework.Models.Actor actor)
        {
            return new Domain.Models.Actor(actor.Name, actor.Surname);
        }
    }
}