using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Model;

namespace Core.Decode
{
    [Serializable]
    public class DecodeHocr
    {
        readonly XName _divName = XName.Get("div");
        
       public List<TextPage> Decode(string text)
        {
            List<TextPage> textPages = new List<TextPage>();

            XDocument textXml = XDocument.Parse(text);

            IEnumerable<XElement> pages = textXml.Descendants(_divName)
                .Where(x => (string) x.Attribute("class") == "ocr_page");

            foreach (XElement page in pages)
            {
                TextPage textPage = new TextPage();

                XAttribute coords = page.Attribute("title");

                if (coords != null)
                {
                    string[] coordlist = coords.Value.Split(' ');

                    textPage.X = Convert.ToInt32(Helpers.GetNumbers(coordlist[3]));
                    textPage.Y = Convert.ToInt32(Helpers.GetNumbers(coordlist[4]));
                    textPage.Width = Convert.ToInt32(Helpers.GetNumbers(coordlist[5]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[3]));
                    textPage.Height = Convert.ToInt32(Helpers.GetNumbers(coordlist[6]))- Convert.ToInt32(Helpers.GetNumbers(coordlist[4]));
                }



                XAttribute id = page.Attribute("id");

                if (id != null) textPage.Id = id.Value;

                List<TextParagraph> textParagraphs = new DecodeParagraphs().Decode(page);

                textPage.Paragraphs.AddRange(textParagraphs);

                textPages.Add(textPage);

            }
            
            return textPages;
        }
    }
}