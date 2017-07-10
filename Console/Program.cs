using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    class Program
    {
        private static MyDictionary _dictionary;
        static void Main()
        {
            _dictionary = new MyDictionary();

            ReadDslFile();
            System.Console.WriteLine("aaa");
            System.Console.ReadKey();

            
        }

        private static void ReadDslFile()
        {
            string filepath = @"D:\pol-pol_SlowGwaryWarszavskiej_1_0.dsl";
            
            var lines = File.ReadAllLines(filepath).Where(arg =>!string.IsNullOrWhiteSpace(arg)).ToArray();

            _dictionary.Name = lines[0].Substring(6);

            _dictionary.Language = lines[1].Substring(17);

            var line = 3;

            for (var i = line; i < lines.Length; i++)
            {
                if (lines[i].Substring(0, 5) == "About")
                {
                    i++;
                    var text = "";

                    var flag = true;

                    while (flag)
                    {
                        if (lines[i].Substring(0, 1) == "\t")
                        {
                            text = text + lines[i].Substring(2);
                            i++;
                        }
                        else
                        {
                            flag = false;
                            
                        }

                    }

                    _dictionary.About = text;
                    line = i;
                }
                break;
            }
            for (int i = line; i < lines.Length; i++)
            {
                var flag = true;
                var _word = lines[i];
                var list =new List<string>();
                i++;
                while (flag)
                {
                    if (lines[i].Substring(0, 1) == "\t")
                    {
                            list.Add(lines[i].Substring(1));
                            i++;
                            }
                    else
                    {
                        flag = false;
                        i--;

                    }    
                        
                   
                }
                _dictionary.Word.Add(_word,list);

            }
        }
    }

    internal class MyDictionary
    {


        public MyDictionary()
        {
            Word = new Dictionary<string, List<string>>();
            
        }

        public string Name { get; set; }
        public string Language { get; set; }
        public string About { get; set; }
        public Dictionary<string, List<string>> Word { get; set; }
    }
}
