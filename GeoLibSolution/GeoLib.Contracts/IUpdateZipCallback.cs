using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IUpdateZipCallback
    {
        [OperationContract(IsOneWay = true)]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        void ZipUpdated(ZipCityData zipCityData);
    }
}
