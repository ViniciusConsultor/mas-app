using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Shipping.Caching
{
    public class CacheProvider : ICacheProvider, IDisposable
    {
        private bool _enabled;
        MemoryCache _cache;

        public CacheProvider(bool enabled = true)
        {
            _enabled = enabled;
            _cache = new MemoryCache("ShippingCache");
        }

        private string FixKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or whitespace.", "key");
            }

            return key.ToUpperInvariant();
        }

        public void Insert(string key, object value)
        {
            if (_enabled)
            {
                if (value != null)
                {
                    if (_cache.Contains(FixKey(key)))
                    {
                        _cache.Remove(FixKey(key));
                    }

                    _cache.Add(FixKey(key), value, null);
                }
            }
        }

        public void Insert(string key, object value, DateTime absoluteExpiration)
        {
            if (_enabled)
            {
                if (value != null)
                {
                    if (_cache.Contains(FixKey(key)))
                    {
                        _cache.Remove(FixKey(key));
                    }

                    _cache.Add(FixKey(key), value, new CacheItemPolicy { AbsoluteExpiration = absoluteExpiration });
                }
            }
        }

        public void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            if (_enabled)
            {
                if (value != null)
                {
                    if (_cache.Contains(FixKey(key)))
                    {
                        _cache.Remove(FixKey(key));
                    }

                    _cache.Add(FixKey(key), value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
                }
            }
        }

        public T Get<T>(string key)
        {
            if (_enabled)
            {
                return (T)_cache.Get(FixKey(key));
            }

            return default(T);
        }

        public bool TryGet<T>(string key, out T value)
        {
            if (_enabled)
            {
                value = Get<T>(FixKey(key));

                if (value == null)
                {
                    return false;
                }

                if (value.Equals(default(T)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        public void Remove(string key)
        {
            if (_enabled)
            {
                _cache.Remove(FixKey(key));
            }
        }

        public void Clear()
        {
            if (_enabled)
            {
                foreach (var item in _cache)
                {
                    _cache.Remove(item.Key);
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            //Check if dispose has already been called.
            if (!this._disposed)
            {
                if (disposing)
                {
                    //Dispose of managed resources.
                    _cache.Dispose();
                }

                //Make sure dispose doesn't run again
                _disposed = true;

            }
        }

        #endregion
    }
}
