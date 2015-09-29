using Ays.PhysicalQuantities.Units;

namespace Ays.PhysicalQuantities
{
    public abstract class Unit
    {
        public double Convert(double value, Unit target)
        {
            double result;

            if (TryConvert(value, target, out result))
                return result;

            throw new Exceptions.ConversionException();
        }

        public abstract bool TryConvert(double value, Unit target, out double result);

        public static Unit NewUnit() => Units.BasicUnit.CreateInstance();

        /// <summary>
        /// Create new unit as multiplation between this unit and double value
        /// 
        /// Example:
        ///     Kilometers = Meters.Multiply(1000);
        /// 
        /// </summary>
        /// <param name="value">multiply ratio</param>
        /// <returns>this unit multiplied by value</returns>
        internal abstract Unit Multiply(double value);

        /// <summary>
        /// Create new unit as dividition between this unit and double value
        /// 
        /// Example:
        ///     Meters = Kilometers.Divide(1000);
        /// 
        /// </summary>
        /// <param name="value">divide ratio</param>
        /// <returns>this unit divided by value</returns>
        internal abstract Unit Divide(double value);

        /// <summary>
        /// Example Usange:
        ///     Unit x = Unit.NewUnit();
        ///     Unit y = x.Add(10);
        /// 
        ///     10 x = 0 y
        /// </summary>
        internal abstract Unit Add(double value);

        /// <summary>
        /// Example Usange:
        ///     Unit x = Unit.NewUnit();
        ///     Unit y = x.Negate();
        /// 
        ///     10 x = -10 y
        /// </summary>
        internal abstract Unit AdditiveInverse();


        #region Operators

        public static Unit operator *(Unit unit, double value) => unit.Multiply(value);
        
        public static Unit operator *(double value, Unit unit) => unit.Multiply(value);

        public static Unit operator /(Unit unit, double value) => unit.Divide(value);

        public static Unit operator +(double value, Unit unit) => unit.Add(value);

        public static Unit operator +(Unit unit, double value) => unit.Add(value);

        public static Unit operator -(Unit unit, double value) => unit.Add(-value);

        public static Unit operator -(double value, Unit unit) => unit.AdditiveInverse().Add(value);

        public static Unit operator -(Unit unit) => unit.AdditiveInverse();

        public static Unit operator *(Unit left, Unit right) => DerivedUnit.CreateInstance(left, right, DerivedUnit.AggregationType.Multiply);

        public static Unit operator /(Unit left, Unit right) => DerivedUnit.CreateInstance(left, right, DerivedUnit.AggregationType.Divide);

        #endregion Operators
    }
}
 