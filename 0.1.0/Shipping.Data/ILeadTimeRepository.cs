using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Data
{
    public interface ILeadTimeRepository
    {
        bool CreateLeadTime(Shipping.Business.Entities.LeadTime leadTime);
        bool EditLeadTime(Shipping.Business.Entities.LeadTime leadTime);
        bool DeleteLeadTime(string ID);
        IEnumerable<Shipping.Business.Entities.LeadTime> GetListLeadTime();
        Shipping.Business.Entities.LeadTime GetLeadTimeByID(string ID);
    }
}
