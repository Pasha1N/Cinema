using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Data
{
    public interface IFilmDataService
    {
        IFilmDataGateway OpenDataGateway();
    }
}