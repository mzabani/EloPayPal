using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EloPayPal;

namespace Tests
{
    [TestFixture]
    class Rounding
    {
        [Test]
        public void Round50CentsUp()
        {
            Assert.AreEqual(0.00M, MoneyUtils.RoundTo50CentsUp(0));
            Assert.AreEqual(0.50M, MoneyUtils.RoundTo50CentsUp(0.49M));

            Assert.AreEqual(11.50M, MoneyUtils.RoundTo50CentsUp(11.499999M));
            Assert.AreEqual(12.00M, MoneyUtils.RoundTo50CentsUp(11.51M));
            Assert.AreEqual(12.00M, MoneyUtils.RoundTo50CentsUp(12.00M));
            Assert.AreEqual(12.50M, MoneyUtils.RoundTo50CentsUp(12.00001M));
        }

        [Test]
        public void Round10CentsUp()
        {
            Assert.AreEqual(0M, MoneyUtils.RoundTo10CentsUp(0));
            Assert.AreEqual(.10M, MoneyUtils.RoundTo10CentsUp(.099M));
            Assert.AreEqual(.10M, MoneyUtils.RoundTo10CentsUp(.10M));
            Assert.AreEqual(.20M, MoneyUtils.RoundTo10CentsUp(.10000001M));
            Assert.AreEqual(.20M, MoneyUtils.RoundTo10CentsUp(.11M));
            Assert.AreEqual(12.40M, MoneyUtils.RoundTo10CentsUp(12.40M));
            Assert.AreEqual(12.50M, MoneyUtils.RoundTo10CentsUp(12.41M));
        }
    }
}
