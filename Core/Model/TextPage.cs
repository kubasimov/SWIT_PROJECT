using System.Collections.Generic;

namespace Core.Model
{
    public class TextPage : Coord
    {
        public string Id;
        public List<TextParagraph> Paragraphs = new List<TextParagraph>();
    }

}