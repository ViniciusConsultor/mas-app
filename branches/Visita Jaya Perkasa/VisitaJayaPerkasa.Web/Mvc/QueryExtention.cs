using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VisitaJayaPerkasa.Web.Mvc
{
    public static class QueryExtention
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> query, string dataValueField, string dataTextField, object selectedValue)
        {
            return new SelectList(query, dataValueField, dataTextField, selectedValue ?? -1);
        }
    }
}
