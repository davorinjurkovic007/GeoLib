using GeoLib.Contracts;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoLib.Proxies
{
    public class GeoAdminClient : ClientBase<IGeoAdminService>, IGeoAdminService
    {
        public GeoAdminClient()
        {
        }

        public GeoAdminClient(string endpointName)
            : base(endpointName) 
        {
        }

        public GeoAdminClient(Binding binding, EndpointAddress address)
            : base(binding, address) 
        {
        }

        public void UpdateZipCity(string zip, string city)
        {
            Channel.UpdateZipCity(zip, city);
        }

        public void UpdateZipCity(IEnumerable<ZipCityData> zipCitydata)
        {
            Channel.UpdateZipCity(zipCitydata);
        }
    }
}
