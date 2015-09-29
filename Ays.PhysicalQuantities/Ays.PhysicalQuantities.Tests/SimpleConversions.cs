using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ays.PhysicalQuantities.Tests
{
    [TestClass]
    public class SimpleConversions
    {
        [TestMethod]
        public void Conversions_MultiplyBasicUnits()
        {
            Unit m = Unit.NewUnit();

            Assert.AreEqual(0.123321d, m.Convert(123.321, 1000 * m), "#1");
            Assert.AreEqual(0.32415d, m.Convert(324.15d, m * 1000), "#2");
            Assert.AreEqual(500, ((1000 * m) * 5).Convert(0.1, m), "#3");
            Assert.AreEqual(1, ((1000 * m) * 0.001).Convert(1, m), "#4");

            Unit km = 1000 * m;
            Unit feet = 0.3048 * m;

            Assert.AreEqual(37.4904, feet.Convert(123, m), "#5");
            Assert.AreEqual(0.0374904, feet.Convert(123, km), "#6");
            Assert.AreEqual(3280.84, Math.Round(km.Convert(1, feet), 2), "#7");
        }

        [TestMethod]
        public void Conversions_NegationBasicUnits()
        {
            Unit m = Unit.NewUnit();

            Assert.AreEqual(-0.123, m.Convert(0.123, -m), "#1");
            Assert.AreEqual(-0.123, -m.Convert(0.123, m), "#2");
            Assert.AreEqual(123.321, (-m).Convert(m.Convert(123.321, -m), m), "#3");
        }

        [TestMethod]
        public void Conversions_AdditionBasicUnits()
        {
            Unit m = Unit.NewUnit();

            Assert.AreEqual(-9.877, m.Convert(0.123, m + 10), "#1");
            Assert.AreEqual(0.123, Math.Round((m + 10).Convert(-9.877, m), 3), "#2");

            Assert.AreEqual(-19.877, Math.Round(m.Convert(0.123, m + 10 + 10), 3), "#3");
            Assert.AreEqual(0.123, Math.Round((m + 10 + 10).Convert(-19.877, m), 3), "#4");

            Assert.AreEqual(15.323, m.Convert(10.323, m + (-5)), "#5");
            Assert.AreEqual(15.323, m.Convert(10.323, m - 5), "#6");

            Assert.AreEqual(-15.323, m.Convert(10.323, -m + 5), "#7");
            Assert.AreEqual(m.Convert(10.323, 5 - m), m.Convert(10.323, -m + 5), "#8");
            Assert.AreEqual(m.Convert(10.323, 5 - (m + 10)), m.Convert(10.323, -(m + 10) + 5), "#9");
        }

        [TestMethod]
        public void Conversions_Fahrenheit()
        {
            Unit c = Unit.NewUnit();
            Unit k = (c - 32) / 1.8;

            Assert.AreEqual(50, c.Convert(10, k), "#1");
            Assert.AreEqual(10, k.Convert(50, c), "#2");
        }

        [TestMethod]
        public void Conversions_SqueredUnits()
        {
            Unit m = Unit.NewUnit();
            Unit km = m * 1000;

            Unit m2 = m * m;
            Unit km2 = km * km;

            Assert.AreEqual(1, m2.Convert(1000000, km2), "#1");
            Assert.AreEqual(1000000, km2.Convert(1, m2), "#1");
        }
    }
}
