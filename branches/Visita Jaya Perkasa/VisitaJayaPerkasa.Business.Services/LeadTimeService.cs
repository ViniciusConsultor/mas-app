using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class LeadTimeService : ILeadTimeService
    {
        private readonly ILeadTimeRepository _LeadTimeRepository;
        public LeadTimeService(ILeadTimeRepository leadTimeRepository){
            _LeadTimeRepository = leadTimeRepository;
        }

        public void SaveLeadTime(LeadTime leadTime)
        {
            _LeadTimeRepository.SaveLeadTime(leadTime);
        }

        public void DeleteLeadTime(string ID)
        {
            _LeadTimeRepository.DeleteLeadTime(ID);
        }

        public IEnumerable<LeadTime> GetListLeadTime()
        {
            return _LeadTimeRepository.GetListLeadTime();
        }

        public LeadTime GetLeadTimeByID(string ID)
        {
            return _LeadTimeRepository.GetLeadTimeByID(ID);
        }
    }
}
