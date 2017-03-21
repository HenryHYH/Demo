using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDK.Tests
{
    public class LogicTest
    {
        [Fact]
        public void AndShortcutTest()
        {
            int num = 0;
            bool val = true;

            val = val && ReturnFalse(ref num)
                      && ReturnTrue(ref num);

            Assert.Equal(false, val);
            Assert.Equal(1, num);
        }

        private bool ReturnTrue(ref int num)
        {
            num += 1;

            return true;
        }

        private bool ReturnFalse(ref int num)
        {
            num += 1;

            return false;
        }
    }
}
