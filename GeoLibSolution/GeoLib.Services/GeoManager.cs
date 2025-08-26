using GeoLib.Contracts;
using GeoLib.Data.Entities;
using GeoLib.Data.Repositories;
using GeoLib.Data.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;

namespace GeoLib.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GeoManager : IGeoService
    {
        public GeoManager()
        {
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
            _StateRepository = stateRepository;
        }

        IZipCodeRepository _ZipCodeRepository = null;
        IStateRepository _StateRepository = null;

        int _Counter = 0;

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            List<string> stateData = new List<string>();

            IStateRepository stateRepository = _StateRepository ?? new StateRepository();

            IEnumerable<State> states = stateRepository.Get(primaryOnly);
            if (states != null)
            {
                foreach (State state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }

            return stateData;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            //Thread.Sleep(10000);

            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }
            else
            {
                //throw new ApplicationException(string.Format("Zip code {0} not found.", zip));

                //throw new FaultException(string.Format("Zip code {0} not found.", zip));

                //ApplicationException ex = new ApplicationException(string.Format("Zip code {0} not found.", zip));
                //throw new FaultException<ApplicationException>(ex, "Just another message");

                NotFoundData data = new NotFoundData()
                {
                    Message = string.Format("Zip code {0} not found.", zip),
                    When = DateTime.UtcNow.ToString(),
                    User = "Majka božja"
                };

                throw new FaultException<NotFoundData>(data, "Just another message.");
            }

                _Counter++;
            Console.WriteLine("Counter = {0}", _Counter.ToString());

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            var zips = zipCodeRepository.GetByState(state);
            if (zips != null)
            {
                foreach(ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData() 
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);
            if(zips != null)
            {
                foreach(ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData 
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode=zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }
    }
}
