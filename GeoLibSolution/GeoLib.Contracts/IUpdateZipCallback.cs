using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IUpdateZipCallback
    {
        [OperationContract(IsOneWay = false)]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        void ZipUpdated(ZipCityData zipCityData);
    }
}
