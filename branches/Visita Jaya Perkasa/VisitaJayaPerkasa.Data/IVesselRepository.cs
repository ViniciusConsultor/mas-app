using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface IVesselRepository
    {
        void SaveVessel(Vessel vessel);
        void DeleteVessel(string ID);
        IEnumerable<Vessel> GetVessels();
        Vessel GetVesselByID(string ID);
    }
}
