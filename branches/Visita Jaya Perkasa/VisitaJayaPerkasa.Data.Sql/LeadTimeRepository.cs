using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class LeadTimeRepository : ILeadTimeRepository
    {
        private readonly string _mainConnectionString;

        public LeadTimeRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveLeadTime(LeadTime leadTime)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetLeadTimeByID(leadTime.CityCode) == null)
                {
                    //Create new
                    repo.Insert(leadTime);
                }
                else
                {
                    //Update it

                    repo.Update("Lead_Time", "city_code", leadTime);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteLeadTime(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            LeadTime leadtime = GetLeadTimeByID(ID);
            repo.Delete("Lead_Time", "city_code", leadtime);
            repo.CloseSharedConnection();  
        }

        public IEnumerable<LeadTime> GetListLeadTime()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<LeadTime> leadtime = repo.Fetch<LeadTime>("SELECT * FROM [Lead_Time] WHERE (delete is null OR deleted = '0')").ToList<LeadTime>();

            repo.CloseSharedConnection();

            return leadtime;
        }

        public LeadTime GetLeadTimeByID(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            LeadTime leadtime = repo.SingleOrDefault<LeadTime>("SELECT * FROM lead_time WHERE city_code=@0 AND (deleted is null OR deleted = '0')", ID);

            repo.CloseSharedConnection();

            return leadtime;
        }


        public IEnumerable<LeadTime> GetLeadTimeBySearch(string searchWord)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<LeadTime> listLeadTime = repo.Fetch<LeadTime>(
                "SELECT * FROM Lead_Time WHERE (deleted is null OR deleted = '0') AND " + 
                "(city_code like '%" + searchWord + "%' OR days like '%" + searchWord + "%')"
                ).ToList();

            repo.CloseSharedConnection();
            return listLeadTime;
        }
    }
}
