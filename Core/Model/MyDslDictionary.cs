using System.Collections.Generic;

namespace Core.Model
{
    public class MyDslDictionary
    {
        public MyDslDictionary()
        {
            Word = new Dictionary<string, List<string>>();

        }

        public string Name { get; set; }
        public string Language { get; set; }
        public string About { get; set; }
        public Dictionary<string, List<string>> Word { get; set; }
    }
}