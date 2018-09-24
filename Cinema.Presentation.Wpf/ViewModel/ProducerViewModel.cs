using Films.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModel
{
    internal class ProducerViewModel
    {
        private Producer producer;

        public ProducerViewModel(Producer producer)
        {
            this.producer = producer;
        }

        public string Name => producer.Name;
        public Producer Producer => producer;
        public string Surname => producer.Surname;
    }
}