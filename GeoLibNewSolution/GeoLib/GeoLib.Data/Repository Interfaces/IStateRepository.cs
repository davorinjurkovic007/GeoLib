using GeoLib.Core;
using GeoLib.Data.Entities;
using System.Collections.Generic;

namespace GeoLib.Data.Repository_Interfaces
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
