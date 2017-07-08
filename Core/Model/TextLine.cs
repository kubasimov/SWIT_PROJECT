using System.Collections.Generic;

namespace Core.Model
{
    public class TextLine : Coord
    {
        public string id;

        public List<TextWord> Words = new List<TextWord>();
    }
}