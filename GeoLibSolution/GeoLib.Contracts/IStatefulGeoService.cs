using System.Collections.Generic;
using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IStatefulGeoService
    {
        [OperationContract]
        void PushZip(string zip);
        [OperationContract(IsInitiating = false)]
        ZipCodeData GetZipInfo();
        [OperationContract(IsInitiating = false)]
        IEnumerable<ZipCodeData> GetZips(int range);
    }
}
