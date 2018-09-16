namespace Movie.Domain.Models
{
    public class ReleaseDate
    {
        private int year;
        private int month;
        private int day;

        public ReleaseDate(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public int Year => year;
        public int Month => month;
        public int Day => day;
    }
}