using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Business.Entities;

namespace Shipping.Business.Services.ConditionService
{
    public interface IConditionService
    {
        bool CreateCondition(Condition condition);
        bool EditCondition(Condition condition);
        bool DeleteCondition(string conditionCode);
        IEnumerable<Condition> GetListCondition();
        Condition GetConditionByID(string conditionCode);
    }
}
