using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Business.Services
{
    public class TypeContService : ITypeContService
    {
        private readonly ITypeContRepository _typeContRepository;
        public TypeContService(ITypeContRepository typeContRepository) {
            _typeContRepository = typeContRepository;
        }

        public void SaveTypeCont(TypeCont typeCont)
        {
            _typeContRepository.SaveTypeCont(typeCont);
        }

        public void DeleteTypeCont(string ID)
        {
            _typeContRepository.DeleteTypeCont(ID);
        }

        public IEnumerable<TypeCont> GetListTypeCont()
        {
            return _typeContRepository.GetListTypeCont();
        }

        public TypeCont GetTypeContByID(string ID)
        {
            return _typeContRepository.GetTypeContByID(ID);
        }
    }
}
