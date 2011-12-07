using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class VesselService : IVesselService
    {
        private readonly IVesselRepository _vesselRepository;

        public VesselService(IVesselRepository vesselRepository)
        {
            _vesselRepository = vesselRepository;
        }
        #region IVesselService Members

        public void SaveVessel(Vessel vessel)
        {
            _vesselRepository.SaveVessel(vessel);
        }

        public void DeleteVessel(string ID)
        {
            _vesselRepository.DeleteVessel(ID);
        }

        public IEnumerable<Vessel> GetVessels()
        {
            return _vesselRepository.GetVessels();
        }

        public Vessel GetVesselByID(string ID)
        {
            return _vesselRepository.GetVesselByID(ID);
        }

        #endregion
    }
}
