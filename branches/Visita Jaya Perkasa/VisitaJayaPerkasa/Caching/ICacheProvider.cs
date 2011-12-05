using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Caching
{
    public interface ICacheProvider
    {
        void Insert(string key, object value);

        void Insert(string key, object value, DateTime absoluteExpiration);

        void Insert(string key, object value, TimeSpan slidingExpiration);

        T Get<T>(string key);

        bool TryGet<T>(string key, out T value);

        void Remove(string key);

        void Clear();
    }
}
