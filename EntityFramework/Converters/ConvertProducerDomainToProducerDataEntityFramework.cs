namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertProducer
    {
        public static EntityFramework.Models.Producer ToEntityFrameworkModelsProducer(Films.Domain.Models.Producer producerDomain)
        {
            EntityFramework.Models.Producer producer = new Models.Producer();
            producer.Name = producerDomain.Name;
            producer.Surname = producerDomain.Surname;

            return producer;
        }

        public static Films.Domain.Models.Producer ToModelsProducer(EntityFramework.Models.Producer producer)
        {
            return new Domain.Models.Producer(producer.Name, producer.Surname);
        }
    }
}