using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.Data
{
    public interface ITypeContRepository
    {
        bool CreateTypeCont(Shipping.Business.Entities.TypeCont typeCont);
        bool EditTypeCont(Shipping.Business.Entities.TypeCont typeCont);
        bool DeleteTypeCont(string ID);
        IEnumerable<Shipping.Business.Entities.TypeCont> GetListTypeCont();
        Shipping.Business.Entities.TypeCont GetTypeContByID(string ID);
    }
}
