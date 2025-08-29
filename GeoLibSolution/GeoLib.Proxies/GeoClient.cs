using GeoLib.Contracts;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoLib.Proxies
{
    //public class GeoClient : DuplexClientBase<GeoLib.Contracts.IGeoService>, GeoLib.Contracts.IGeoService
    //{
    //    public GeoClient(InstanceContext instanceContext) 
    //        : base(instanceContext)
    //    {
    //    }

    //    public GeoClient(InstanceContext instanceContext, string endpointName) 
    //        : base(instanceContext, endpointName) { }

    //    public GeoClient(InstanceContext instanceContext, Binding binding, EndpointAddress address) 
    //        : base(instanceContext, binding, address) { }

    //    public IEnumerable<string> GetStates(bool primaryOnly)
    //    {
    //        return Channel.GetStates(primaryOnly);
    //    }

    //    public ZipCodeData GetZipInfo(string zip)
    //    {
    //        return Channel.GetZipInfo(zip);
    //    }

    //    public IEnumerable<ZipCodeData> GetZips(string state)
    //    {
    //        return Channel.GetZips(state);
    //    }

    //    public IEnumerable<ZipCodeData> GetZips(string zip, int range)
    //    {
    //        return Channel.GetZips(zip, range);
    //    }

    //    public void UpdateZipCity(string zip, string city)
    //    {
    //        Channel.UpdateZipCity(zip, city);
    //    }

    //    public int UpdateZipCity(IEnumerable<ZipCityData> zipCityData)
    //    {
    //        return Channel.UpdateZipCity(zipCityData);

    //    }

    //    public void OneWayExample()
    //    {
    //        Channel.OneWayExample();
    //    }
    //}
}
