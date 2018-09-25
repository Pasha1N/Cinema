namespace Films.Domain.Models
{
    public class Producer
    {
        private string name = string.Empty;
        private string surname = string.Empty;

        public Producer(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;
        public string Surname => surname;
    }
}