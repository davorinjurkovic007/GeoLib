using GeoLib.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GeoLib.Proxies
{
    [ServiceContract(CallbackContract = typeof(IUpdateZipCallback))]
    public interface IGeoService
    {
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        [FaultContract(typeof(NotFoundData))]
        ZipCodeData GetZipInfo(string zip);

        [OperationContract]
        IEnumerable<string> GetStates(bool primaryOnly);

        [OperationContract(Name = "GetZipsByState")]
        IEnumerable<ZipCodeData> GetZips(string state);

        [OperationContract(Name = "GetZipsForRange")]
        IEnumerable<ZipCodeData> GetZips(string zip, int range);

        [OperationContract]
        void UpdateZipCity(string zip, string city);

        [OperationContract(Name = "UpdateZipCityBatch")]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int UpdateZipCity(IEnumerable<ZipCityData> zipCityData);

        [OperationContract(Name = "UpdateZipCityBatch")]
        Task<int> UpdateZipCityAsync(IEnumerable<ZipCityData> zipCityData);

        [OperationContract(IsOneWay = true)]
        void OneWayExample();

    }
}
