using System.Collections.Generic;
using Core.Model;

namespace Core.Interface
{
    public interface IDslDictionaries
    {
        
        Dictionary<string, string> SearchWordInDslDictionaries(string word);
    }
}