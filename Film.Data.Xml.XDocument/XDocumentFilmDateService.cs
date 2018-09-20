using Film.DataXmlXDocument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Film.Data.XmlXDocument
{
    public class XDocumentFilmDateService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new XDocumentFilmDataGateway();
        }
    }
}