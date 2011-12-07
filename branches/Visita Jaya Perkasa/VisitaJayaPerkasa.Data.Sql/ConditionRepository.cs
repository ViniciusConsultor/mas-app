using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly string _mainConnectionString;
        public ConditionRepository(string mainConnectionString) {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveCondition(Condition condition)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetConditionByID(condition.ConditionCode) == null)
                {
                    //Create new
                    repo.Insert(condition);
                }
                else
                {
                    //Update it

                    repo.Update("CONDITION", "condition_code", condition);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteCondition(string conditionCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Condition condition = GetConditionByID(conditionCode);
            repo.Delete("CONDITION", "condition_code", condition);
            repo.CloseSharedConnection(); 
        }

        public IEnumerable<Condition> GetListCondition()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Condition> conditions = repo.Fetch<Condition>("SELECT * FROM [Condition] WHERE (deleted is null OR deleted = '0')").ToList<Condition>();

            repo.CloseSharedConnection();

            return conditions;
        }

        public Condition GetConditionByID(string conditionCode)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Condition conditions = repo.SingleOrDefault<Condition>("SELECT * FROM Condition WHERE condition_code=@0 AND (deleted is null OR deleted = '0')", conditionCode);

            repo.CloseSharedConnection();

            return conditions;
        }


        public IEnumerable<Condition> GetConditionBySearch(string searchWord)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<Condition> listCondition = repo.Fetch<Condition>(
                "SELECT * FROM condition WHERE (deleted is null OR deleted = '0') AND " + 
                "(condition_code like '%" + searchWord + "%' OR condition_name like '%" + searchWord + "%')"
                ).ToList();

            repo.CloseSharedConnection();
            return listCondition;
        }
    }
}
