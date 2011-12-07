using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Data;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class TypeContRepository : ITypeContRepository
    {
        private readonly string _mainConnectionString;
        public TypeContRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveTypeCont(TypeCont typeCont)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetTypeContByID(typeCont.TypeCode) == null)
                {
                    //Create new
                    repo.Insert(typeCont);
                }
                else
                {
                    //Update it

                    repo.Update("TYPE_CONT", "type_code", typeCont);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();
        }

        public void DeleteTypeCont(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            TypeCont typeCont = GetTypeContByID(ID);
            repo.Delete("TYPE_CONT", "type_code", typeCont);
            repo.CloseSharedConnection();
        }

        public IEnumerable<TypeCont> GetListTypeCont()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<TypeCont> categories = repo.Fetch<TypeCont>("SELECT * FROM [TYPE_CONT]").ToList<TypeCont>();

            repo.CloseSharedConnection();

            return categories;
        }

        public TypeCont GetTypeContByID(string ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            TypeCont typeCont = repo.SingleOrDefault<TypeCont>("SELECT * FROM TYPE_CONT WHERE type_code=@0", ID);

            repo.CloseSharedConnection();

            return typeCont;

        }
    }
}
