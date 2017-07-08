using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Model;

namespace Core.Decode
{
    public class DecodeParagraphs
    {
        XName _paraName = XName.Get("p");

        public List<TextParagraph> Decode(XElement element)
        {
            List<TextParagraph> textParagraphs = new List<TextParagraph>();

            IEnumerable<XElement> paragraphs = element.Descendants(_paraName)
                .Where(x => (string)x.Attribute("class") == "ocr_par");

            foreach (XElement paragraph in paragraphs)
            {
                TextParagraph textParagraph = new TextParagraph();

                XAttribute coords = paragraph.Attribute("title");
                if (coords != null)
                {
                    string[] coordlist = coords.Value.Split(' ');

                    textParagraph.X = Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textParagraph.Y = Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                    textParagraph.Width = Convert.ToInt32(Helpers.GetNumbers(coordlist[3]))-Convert.ToInt32(Helpers.GetNumbers(coordlist[1]));
                    textParagraph.Height = Convert.ToInt32(Helpers.GetNumbers(coordlist[4]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[2]));
                }

                XAttribute id = paragraph.Attribute("id");

                if (id != null) textParagraph.id = id.Value;

                List<TextLine> textLines = new DecodeLines().Decode(paragraph);

                textParagraph.Lines.AddRange(textLines);

                textParagraphs.Add(textParagraph);
            }

            return textParagraphs;
        }
    }
}