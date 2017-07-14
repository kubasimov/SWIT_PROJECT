using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Interface;
using Core.Model;

namespace Core.Implement
{
    public class DslDictionaries:IDslDictionaries
    {
        private readonly List<MyDslDictionary> _dslDictionaries;
        //private List<string> _filenameList;

        public DslDictionaries()
        {
            //ReadAllFileFromDirectory
            var filenames = Directory
                .GetFiles(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Dictionary\",
                    "*.dsl")
                .Select(Path.GetFullPath);

            //AddAllWordFromFileToMyDslDictionary

            _dslDictionaries=new List<MyDslDictionary>();

            foreach (var filename in filenames)
            {
                _dslDictionaries.Add(Read(filename));
            }


        }

        public Dictionary<string,string> SearchWordInDslDictionaries(string word)
        {
            
            var wordsDictionary = new Dictionary<string, string>();

            foreach (var myDslDictionary in _dslDictionaries)
            {
                foreach (var wordKeyValuePair in myDslDictionary.Word)
                {
                    if (wordKeyValuePair.Key.ToLower() == word.ToLower())
                    {
                        
                        


                        wordsDictionary.Add(myDslDictionary.Name,string.Join(",\n\t",wordKeyValuePair.Value));
                    }
                }
            }

            return wordsDictionary;
        }


        private MyDslDictionary Read(string filename)
        {
            var dictionary= new MyDslDictionary();

            var lines = File.ReadAllLines(filename).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();

            dictionary.Name = lines[0].Substring(6);

            dictionary.Language = lines[1].Substring(17);

            var line = 3;

            for (var i = line; i < lines.Length;)
            {
                if (lines[i].Substring(0, 5) == "About"||lines[i].Substring(0,5)== "O sło")
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

                    dictionary.About = text;
                    line = i;
                }
                break;
            }
            for (int i = line; i < lines.Length; i++)
            {
                var flag = true;
                var word = lines[i];
                var list = new List<string>();
                i++;
                while (flag)
                {

                    if (i == lines.Length) break;

                    if (lines[i].Substring(0, 1) == "\t")
                    {
                        list.Add(lines[i].Substring(1).Replace("[m2]",string.Empty));
                        i++;
                    }
                    else
                    {
                        flag = false;
                        i--;

                    }




                }
                dictionary.Word.Add(word, list);

            }

            return dictionary;
        }
    }
}