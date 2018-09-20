using Movie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Presentation.Wpf.ViewModel
{
    internal class ActorViewModel
    {
        private Actor actor;

        public ActorViewModel(Actor actor)
        {
            this.actor = actor;
        }

        public Actor Actor => actor;
        public string Name => actor.Name;
        public string Surname => actor.Surname;
    }
}