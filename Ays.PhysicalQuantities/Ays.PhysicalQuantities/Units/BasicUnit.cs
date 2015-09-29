using System;

namespace Ays.PhysicalQuantities.Units
{
    public class BasicUnit : Unit
    {
        public static BasicUnit CreateInstance() { return new BasicUnit(); }

        #region Fields

        private readonly Func<double, double> toBaseUnit;
        private readonly Func<double, double> fromBaseUnit;
        private readonly BasicUnit baseUnit;

        #endregion

        #region Constructors

        private BasicUnit()
        {
            toBaseUnit = (d) => d;
            fromBaseUnit = (d) => d;
            baseUnit = this;
        }

        private BasicUnit(BasicUnit baseUnit, double toBaseUnitRatio)
        {
            if (baseUnit == null)
                throw new ArgumentNullException("baseUnit");

            baseUnit = this;
            toBaseUnit = (d) => d * toBaseUnitRatio;
            fromBaseUnit = (d) => d / toBaseUnitRatio;
        }

        private BasicUnit(BasicUnit baseUnit, Func<double, double> toBaseUnit, Func<double, double> fromBaseUnit)
        {
            if (baseUnit == null)
                throw new ArgumentNullException("baseUnit");

            if (toBaseUnit == null)
                throw new ArgumentNullException("toBaseUnit");

            if (fromBaseUnit == null)
                throw new ArgumentNullException("fromBaseUnit");

            this.baseUnit = baseUnit;
            this.toBaseUnit = toBaseUnit;
            this.fromBaseUnit = fromBaseUnit;
        }

        #endregion
        
        #region Implementation of Unit
        
        public override bool TryConvert(double value, Unit target, out double result)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            // set default result
            result = default(double);

            // only supported convertions between basic units
            if (!(target is BasicUnit))
                return false;

            BasicUnit targetBasicUnit = target as BasicUnit;

            if (targetBasicUnit == null)
                return false;

            if (targetBasicUnit.baseUnit != baseUnit)
                return false;

            // set result (convert to base unit, than from base unit to target)
            result = targetBasicUnit.fromBaseUnit(toBaseUnit(value));

            return true;
        }

        internal override Unit Multiply(double value)
        {
            if (value == 0)
                throw new ArgumentException("value", new DivideByZeroException());

            return new BasicUnit(baseUnit, (d) => toBaseUnit(d ) * value, (d) => fromBaseUnit(d / value));
        }

        internal override Unit Divide(double value)
        {
            if(value == 0)
                throw new ArgumentException("value", new DivideByZeroException());

            return new BasicUnit(baseUnit, (d) => toBaseUnit(d) / value, (d) => fromBaseUnit(d * value));
        }

        internal override Unit Add(double value)
        {
            return new BasicUnit(baseUnit, (d) => toBaseUnit(d + value), (d) => fromBaseUnit(d) - value);
        }

        internal override Unit AdditiveInverse()
        {
            return new BasicUnit(baseUnit, (d) => -toBaseUnit(d), (d) => -fromBaseUnit(d));
        }

        #endregion
    }
}
