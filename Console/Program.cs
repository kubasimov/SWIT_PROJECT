using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Console
{
    class Program
    {
        
        static void Main()
        {
            //_dictionary = new MyDictionary();
            string filepath = @"pol-pol_SlowGwaryWarszavskiej_1_0.dsl";

            var tt = new ReadDslFile();

            tt.Read(filepath);
            System . Console.WriteLine("sdasd");
            System.Console.ReadKey();

            
        }
    }

    
}
