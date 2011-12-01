using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Data;
using Shipping.Business.Entities;

namespace Shipping.Business.Services.ConditionService
{
    public class ConditionService : IConditionService
    {
        private readonly IConditionRepository _conditionRepository;
 
        public ConditionService(IConditionRepository conditionRepository) {
            _conditionRepository = conditionRepository;
        }

        public bool CreateCondition(Condition condition)
        {
            return _conditionRepository.CreateCondition(condition);
        }

        public bool EditCondition(Condition condition)
        {
            return _conditionRepository.EditCondition(condition);
        }

        public bool DeleteCondition(string conditionCode)
        {
            return _conditionRepository.DeleteCondition(conditionCode);
        }

        public IEnumerable<Condition> GetListCondition()
        {
            return _conditionRepository.GetListCondition();
        }

        public Condition GetConditionByID(string conditionCode)
        {
            return _conditionRepository.GetConditionByID(conditionCode);
        }
    }
}
