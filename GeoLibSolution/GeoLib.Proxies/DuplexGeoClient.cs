using GeoLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Proxies
{
    public class DuplexGeoClient : DuplexClientBase<IGeoService>, GeoLib.Proxies.IGeoService
    {
        public DuplexGeoClient(InstanceContext instanceContext)
            : base(instanceContext)
        {
        }

        public DuplexGeoClient(InstanceContext instanceContext, string endpointName)
            : base(instanceContext, endpointName)
        {
        }

        public DuplexGeoClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
            : base(instanceContext, binding, address)
        {
        }
        public ZipCodeData GetZipInfo(string zip)
        {
            return Channel.GetZipInfo(zip);
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            return Channel.GetStates(primaryOnly);
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            return Channel.GetZips(state);
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            return Channel.GetZips(zip, range);
        }

        public int UpdateZipCity(IEnumerable<ZipCityData> zipCityData)
        {
            return Channel.UpdateZipCity(zipCityData);
        }

        public Task<int> UpdateZipCityAsync(IEnumerable<ZipCityData> zipCityData)
        {
            return Channel.UpdateZipCityAsync(zipCityData);
        }

        public void UpdateZipCity(string zip, string city)
        {
            throw new NotImplementedException();
        }

        public void OneWayExample()
        {
            throw new NotImplementedException();
        }
    }
}
