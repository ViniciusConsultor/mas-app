using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipping.Data;

namespace Shipping.Business.Services.TypeContService
{
    public class TypeContService : ITypeContService
    {
        private readonly ITypeContRepository _typeContRepository;
        public TypeContService(ITypeContRepository typeContRepository) {
            _typeContRepository = typeContRepository;
        }

        public bool CreateTypeCont(Entities.TypeCont typeCont)
        {
            return _typeContRepository.CreateTypeCont(typeCont);
        }

        public bool EditTypeCont(Entities.TypeCont typeCont)
        {
            return _typeContRepository.EditTypeCont(typeCont);
        }

        public bool DeleteTypeCont(string ID)
        {
            return _typeContRepository.DeleteTypeCont(ID);
        }

        public IEnumerable<Entities.TypeCont> GetListTypeCont()
        {
            return _typeContRepository.GetListTypeCont();
        }

        public Entities.TypeCont GetTypeContByID(string ID)
        {
            return _typeContRepository.GetTypeContByID(ID);
        }
    }
}
