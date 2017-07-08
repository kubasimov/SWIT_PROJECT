using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Decode;
using Core.Model;

namespace Core
{
    public class DecodeLines
    {
        readonly XName _spanName = XName.Get("span");

        public List<TextLine> Decode(XElement paragraph)
        {
            List<TextLine> textLines = new List<TextLine>();

            IEnumerable<XElement> lines = paragraph.Descendants(_spanName)
                .Where(x => (string)x.Attribute("class") == "ocr_line");

            foreach (XElement line in lines)
            {
                TextLine textLine = new TextLine();
                XAttribute coords = line.Attribute("title");
                if (coords != null)
                {
                    string[] coordlist = coords.Value.Split(' ');

                    textLine.X = Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textLine.Y = Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                    textLine.Width = Convert.ToInt32(Helpers.GetNumbers(coordlist[3]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textLine.Height = Convert.ToInt32(Helpers.GetNumbers(coordlist[4]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                }

                XAttribute id = line.Attribute("id");

                if (id != null) textLine.id = id.Value;

                List<TextWord> textWords = new DecodeWords().Decode(line);

                textLine.Words.AddRange(textWords);

                textLines.Add(textLine);
            }

            return textLines;
        }
    }
}