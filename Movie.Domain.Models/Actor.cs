
namespace Movie.Domain.Models
{
    public class Actor
    {
        private string name;
        private string surname;

        public Actor(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;
        public string Surname => surname;
    }
}