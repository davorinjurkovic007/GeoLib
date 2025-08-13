using GeoLib.Core;
using GeoLib.Data.Entities;
using System.Collections.Generic;

namespace GeoLib.Data.Repository_Interfaces
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZip(string zip);
        IEnumerable<ZipCode> GetByState(string state);
        IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range);
    }
}
