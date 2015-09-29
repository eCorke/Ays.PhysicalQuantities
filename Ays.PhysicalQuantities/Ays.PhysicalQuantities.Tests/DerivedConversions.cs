using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ays.PhysicalQuantities.Tests
{
    [TestClass]
    public class DerivedConversions
    {
        [TestMethod]
        public void Conversions_DividedUnits()
        {
            Unit m = Unit.NewUnit();
            Unit km = 1000 * m;
            Unit sec = Unit.NewUnit();
            Unit min = 60 * sec;
            Unit hr = 3600 * sec;

            Unit kmph = km / hr;
            Unit mps = m / sec;
            Unit spm = sec / m;

            Assert.AreEqual(3.6, mps.Convert(1, kmph), "#1");
            Assert.AreEqual(321, Math.Round(kmph.Convert(1155.6, mps), 2), "#2");
            Assert.AreEqual(1, mps.Convert(1, spm), "#3");
            Assert.AreEqual(0.5, mps.Convert(2, spm), "#4");
        }
    }
}
