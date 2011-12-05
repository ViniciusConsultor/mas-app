using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class ConditionService : IConditionService
    {
        private readonly IConditionRepository _conditionRepository;
 
        public ConditionService(IConditionRepository conditionRepository) {
            _conditionRepository = conditionRepository;
        }

        public void SaveCondition(Condition condition)
        {
            _conditionRepository.SaveCondition(condition);
        }

        public void DeleteCondition(string conditionCode)
        {
            _conditionRepository.DeleteCondition(conditionCode);
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
