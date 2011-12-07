using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface ILeadTimeService
    {
        void SaveLeadTime(LeadTime leadTime);
        void DeleteLeadTime(string ID);
        IEnumerable<LeadTime> GetListLeadTime();
        LeadTime GetLeadTimeByID(string ID);
        IEnumerable<LeadTime> GetLeadTimeBySearch(string searchWord);
    }
}
