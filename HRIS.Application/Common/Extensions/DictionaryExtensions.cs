using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue _output;

            if (key == null)
                return default(TValue);

            if (dictionary.TryGetValue(key, out _output))
                return _output;

            return default(TValue);
        }
    }
}
