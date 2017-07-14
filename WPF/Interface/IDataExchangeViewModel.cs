using System;
using WPF.Enum;

namespace WPF.Interface
{
    public interface IDataExchangeViewModel
    {
        Object Item(EnumExchangeViewmodel name);

        bool ContainsKey(EnumExchangeViewmodel name);

        void Add(EnumExchangeViewmodel key, object value);

        void Delete(EnumExchangeViewmodel key);
    }
}