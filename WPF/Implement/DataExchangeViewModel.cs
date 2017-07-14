using System.Collections.Generic;
using WPF.Enum;
using WPF.Interface;

namespace WPF.Implement
{
    public class DataExchangeViewModel : IDataExchangeViewModel
    {
        private readonly Dictionary<EnumExchangeViewmodel, object> _dictionary =
            new Dictionary<EnumExchangeViewmodel, object>();

        public object Item(EnumExchangeViewmodel name)
        {
            object item = _dictionary[name];
            Delete(name);
            return item;
        }

        public bool ContainsKey(EnumExchangeViewmodel name)
        {
            return _dictionary.ContainsKey(name);
        }

        public void Add(EnumExchangeViewmodel key, object value)
        {
            if (ContainsKey(key))
            {
                Delete(key);
            }
            _dictionary.Add(key, value);
        }

        public void Delete(EnumExchangeViewmodel key)
        {
            _dictionary.Remove(key);
        }
    }
}