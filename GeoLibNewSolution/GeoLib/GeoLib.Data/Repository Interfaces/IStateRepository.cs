using GeoLib.Core;
using System.Collections.Generic;

namespace GeoLib.Data
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
