using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Model;

namespace Core.Decode
{
    public class DecodeWords
    {
        readonly XName _spanName = XName.Get("span");

        public List<TextWord> Decode(XElement line)
        {
            List<TextWord> textWords = new List<TextWord>();
            
            IEnumerable<XElement> words = line.Descendants(_spanName);

            foreach (XElement word in words)
            {
                TextWord textWord = new TextWord();

                XAttribute coords = word.Attribute("title");

                if (coords != null)
                {
                    var coordlist = coords.Value.Split(' ');

                    textWord.X = Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textWord.Y = Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                    textWord.Width = Convert.ToInt32(Helpers.GetNumbers(coordlist[3]))-Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textWord.Height = Convert.ToInt32(Helpers.GetNumbers(coordlist[4]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                }

                XAttribute id = word.Attribute("id");
                
                if (word.Elements(XName.Get("strong")).FirstOrDefault(d => !d.IsEmpty) !=null)
                {
                    textWord.Bold = true;
                };
                
                if (id != null) textWord.id = id.Value;
                textWord.Word = word.Value;

                textWords.Add(textWord);
            }

            return textWords;
        }
    }
}