namespace Films.Domain.Models
{
    public class Actor
    {
        private string name = string.Empty;
        private string surname = string.Empty;

        public Actor(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;
        public string Surname => surname;
    }
}