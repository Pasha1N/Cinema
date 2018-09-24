namespace Films.Data
{
    public interface IFilmDataService
    {
        IFilmDataGateway OpenDataGateway();
    }
}