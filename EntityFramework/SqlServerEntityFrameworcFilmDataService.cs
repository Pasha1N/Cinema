using Films.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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