using System.Collections.Generic;

namespace Core.Model
{
    public class TextParagraph : Coord
    {
        public string id;

        public List<TextLine> Lines = new List<TextLine>();
    }

}