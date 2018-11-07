namespace Films.Data.EntityFramework.Converters
{
    public static class ConvertProducerDomainToProducerDataEntityFramework
    {
        public static EntityFramework.Models.Producer ConvertProducer(Films.Domain.Models.Producer producerDomain)
        {
            EntityFramework.Models.Producer producer = new Models.Producer();

            producer.Name = producerDomain.Name;
            producer.Surname = producerDomain.Surname;

            return producer;
        }
    }
}