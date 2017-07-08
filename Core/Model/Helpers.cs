using System.Linq;

namespace Core.Model
{
    public static class Helpers
    {
        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}