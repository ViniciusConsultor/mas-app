﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface ILeadTimeRepository
    {
        void SaveLeadTime(LeadTime leadTime);
        void DeleteLeadTime(string ID);
        IEnumerable<LeadTime> GetListLeadTime();
        LeadTime GetLeadTimeByID(string ID);
    }
}