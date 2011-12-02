using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Services.LeadTimeService;
using Shipping.Data;

namespace Shipping.Business.Services.LeadTime
{
    public class LeadTimeService : ILeadTimeService
    {
        private readonly ILeadTimeRepository _LeadTimeRepository;
        public LeadTimeService(ILeadTimeRepository leadTimeRepository){
            _LeadTimeRepository = leadTimeRepository;
        }

        public bool CreateLeadTime(Entities.LeadTime leadTime)
        {
            return _LeadTimeRepository.CreateLeadTime(leadTime);
        }

        public bool EditLeadTime(Entities.LeadTime leadTime)
        {
            return _LeadTimeRepository.EditLeadTime(leadTime);
        }

        public bool DeleteLeadTime(string ID)
        {
            return _LeadTimeRepository.DeleteLeadTime(ID);
        }

        public IEnumerable<Entities.LeadTime> GetListLeadTime()
        {
            return _LeadTimeRepository.GetListLeadTime();
        }

        public Entities.LeadTime GetLeadTimeByID(string ID)
        {
            return _LeadTimeRepository.GetLeadTimeByID(ID);
        }
    }
}
