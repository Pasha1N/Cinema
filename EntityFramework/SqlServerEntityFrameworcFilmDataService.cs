namespace Films.Data.EntityFramework
{
    public class SqlServerEntityFrameworcFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new SqlServerEntityFrameworkFilmDataGateway();
        }
    }
}