using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    class ReadDslFile
    {
        public  void Read(string filename)
        {
            MyDictionary _dictionary = new MyDictionary();

            var direcotry = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Dictionary\" + filename;

            
            var lines = File.ReadAllLines(direcotry).Where(arg =>!string.IsNullOrWhiteSpace(arg)).ToArray();

            _dictionary.Name = lines[0].Substring(6);

            _dictionary.Language = lines[1].Substring(17);

            var line = 3;

            for (var i = line; i < lines.Length; )
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
                var word = lines[i];
                var list =new List<string>();
                i++;
                while (flag)
                {

                    if (i == lines.Length) break;
                    
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
                _dictionary.Word.Add(word,list);

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