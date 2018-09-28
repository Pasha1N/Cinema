namespace Films.Domain.Models
{
    public class Actor
    {
        private readonly string name = string.Empty;
        private readonly string surname = string.Empty;

        public Actor(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;

        public string Surname => surname;
    }
}