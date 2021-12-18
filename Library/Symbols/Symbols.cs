using System.Collections.Generic;

/* Type that stores symbols in dictionary */

namespace ROELibrary
{
    class Symbols<T>
    {
        Dictionary<T, string> pairs;

        /// <summary>
        ///Set up key-value pairs of symbols
        /// </summary>
        /// <param name="symbols">When in dictionary will be the same values, getKey method will return key to first found value</param>
        public Symbols(Dictionary<T, string> symbols)
        {
            foreach(KeyValuePair<T, string> symbol in symbols)
            {
                pairs.Add(symbol.Key, symbol.Value);
            }
        }

        public string getValue(T key)
        {
            try
            {
                return pairs[key];
            }
            catch (KeyNotFoundException ex)
            {
                var ex2 = new KeyNotFoundException($"Key {key} not found", ex);
                ex2.Data.Add("key", key);
                ex2.Data.Add("Symbol type", typeof(T));

                throw ex2;
            }
        }

        public T getKey(string value)
        {
            foreach (KeyValuePair<T, string> pair in pairs)
            {
                if (pair.Value == value)
                {
                    return pair.Key;
                }
            }

            var ex = new ValueNotFoundException($"Value {value} not found");
            ex.Data.Add("value", value);
            ex.Data.Add("Symbol type", typeof(T));

            throw ex;
        }
    }
}
