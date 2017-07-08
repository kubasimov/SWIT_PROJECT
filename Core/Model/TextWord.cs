using System.Collections.Generic;

namespace Core.Model
{
    public class TextWord : Coord
    {
        public string id;

        public string Word;
        public bool Bold = false;
        public List<TextLetter> Letters = new List<TextLetter>();
    }
}