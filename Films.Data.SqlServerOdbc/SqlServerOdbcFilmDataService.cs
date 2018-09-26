namespace Films.Data.SqlServerOdbc
{
    public class SqlServerOdbcFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new SqlServerOdbcFilmDataGateway();
        }
    }
}