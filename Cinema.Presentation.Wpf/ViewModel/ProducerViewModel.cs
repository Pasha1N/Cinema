using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Presentation.Wpf.ViewModel
{
    internal class ProducerViewModel
    {
        private Producer producer;

        public ProducerViewModel(Producer producer)
        {
            this.producer = producer;
        }

        public Producer Producer => producer;
        public string Name => producer.Name;
        public string Surname => producer.Surname;
    }
}