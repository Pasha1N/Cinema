namespace Films.Data.SqlServerSqlClient
{
    public class SqlServerSqlClientFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new SqlServerSqlClientFilmDataGateway();
        }
    }
}