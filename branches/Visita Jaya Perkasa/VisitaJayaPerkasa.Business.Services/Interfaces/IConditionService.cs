using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public interface IConditionService
    {
        void SaveCondition(Condition condition);
        void DeleteCondition(string conditionCode);
        IEnumerable<Condition> GetListCondition();
        Condition GetConditionByID(string conditionCode);
        IEnumerable<Condition> GetConditionBySearch(string searchWord);
    }
}
