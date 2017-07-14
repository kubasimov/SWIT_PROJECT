using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Console
{
    public class Tests
    {
        [Fact]
       public void ReadSubString()
        {
            var text = "\tKaczka";
            var sub = text.Substring(0,1);

            Assert.Equal("\t",sub);
        }

        [Fact]
        public void AddNames()
        {
            List<string> ll = new List<string>{"aaa","bbb","ccc"};
            var t = string.Join(", ", ll);
            Assert.Equal("aaa, bbb, ccc",t);
        }
    }
}
