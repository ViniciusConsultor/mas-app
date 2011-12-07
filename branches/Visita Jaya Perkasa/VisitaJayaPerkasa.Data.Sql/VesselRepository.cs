using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class VesselRepository : IVesselRepository
    {
        private string _mainConnectionString;

        public VesselRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }
        #region IVesselRepository Members

        public void SaveVessel(Vessel vessel)
        {
             var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetVesselByID(vessel.VesselCode) == null)
                {
                    //Create new
                    repo.Insert(vessel);
                }
                else
                {
                    //Update it

                    repo.Update("VESSEL", "vessel_code", vessel);
                }

                scope.Complete();
            }
            repo.CloseSharedConnection();
        }

        public void DeleteVessel(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Vessel vessel = GetVesselByID(ID);
            repo.Delete("VESSEL", "vessel_code", vessel);
            repo.CloseSharedConnection();  
        }

        public IEnumerable<Vessel> GetVessels()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Vessel> categories = repo.Fetch<Vessel>("SELECT * FROM [VESSEL]  WHERE (deleted is null OR deleted = '0')").ToList<Vessel>();

            repo.CloseSharedConnection();

            return categories;
        }

        public Vessel GetVesselByID(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Vessel vessel = repo.SingleOrDefault<Vessel>("SELECT * FROM VESSEL WHERE vessel_code=@0", ID);

            repo.CloseSharedConnection();

            return vessel;
        }

        #endregion
    }
}
