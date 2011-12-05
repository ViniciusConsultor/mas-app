using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data
{
    public interface ITypeContRepository
    {
        void SaveTypeCont(TypeCont typeCont);
        void DeleteTypeCont(string ID);
        IEnumerable<TypeCont> GetListTypeCont();
        TypeCont GetTypeContByID(string ID);
    }
}
