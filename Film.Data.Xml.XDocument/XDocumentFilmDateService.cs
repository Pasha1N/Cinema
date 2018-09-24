namespace Films.Data.Xml.xDocument
{
    public class XDocumentFilmDateService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new XDocumentFilmDataGateway();
        }
    }
}