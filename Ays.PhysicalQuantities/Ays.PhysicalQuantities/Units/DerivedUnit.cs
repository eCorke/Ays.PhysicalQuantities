using System;

namespace Ays.PhysicalQuantities.Units
{
    internal class DerivedUnit : Unit
    {
        private readonly Unit left;
        private readonly Unit right;
        private readonly AggregationType aggregation;

        public static Unit CreateInstance(Unit left, Unit right, AggregationType aggregation)
        {
            return new DerivedUnit(left, right, aggregation);
        }

        private DerivedUnit(Unit left, Unit right, AggregationType aggregation)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            if (right == null)
                throw new ArgumentNullException("right");

            this.left = left;
            this.right = right;
            this.aggregation = aggregation;
        }

        public override bool TryConvert(double value, Unit target, out double result)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            // set default result
            result = default(double);

            // only supported convertions between derived units
            if (!(target is DerivedUnit))
                return false;

            DerivedUnit targetDerivedUnit = target as DerivedUnit;

            // don't know how to convert derived unit without same aggregation type
            if (aggregation != targetDerivedUnit.aggregation)
                return false;

            double leftResult;
            double rightResult;
            bool inverted = false;

            // if left not convertible to target.left
            if(!left.TryConvert(value, targetDerivedUnit.left, out leftResult))
            {
                // if left not not convertible to target.right return
                if (!left.TryConvert(value, targetDerivedUnit.right, out leftResult))
                    return false;

                // if left convertible to target.right and right not convertible to target.left return
                else if (!right.TryConvert(value, targetDerivedUnit.left, out rightResult))
                    return false;

                inverted = true;
            }
            // if left convertible to target.left and right not convertible to target.right
            else if (!right.TryConvert(value, targetDerivedUnit.right, out rightResult))
                return false;

            // calculation
            switch (aggregation)
            {
                case AggregationType.Divide:
                    result = ((value * leftResult) / rightResult);

                    if (inverted)
                        result = 1d / result;

                    break;

                case AggregationType.Multiply:
                    result = (leftResult * rightResult) / value;
                    break;

                default:
                    throw new NotImplementedException(aggregation.ToString());
            }

            return true;
        }

        internal override Unit Add(double value)
        {
            throw new NotImplementedException("Not implemented yet.");
        }

        internal override Unit AdditiveInverse()
        {
            throw new NotImplementedException("Not implemented yet.");
        }

        internal override Unit Divide(double value)
        {
            if (aggregation == AggregationType.Multiply)
                return new DerivedUnit(left, right.Divide(value), AggregationType.Multiply);

            if (aggregation == AggregationType.Divide)
                return new DerivedUnit(left, right.Multiply(value), AggregationType.Divide);

            throw new NotSupportedException(aggregation.ToString());
        }

        internal override Unit Multiply(double value)
        {
            return new DerivedUnit(left.Multiply(value), right, aggregation);
        }

        public enum AggregationType
        {
            Multiply = 1,
            Divide = 2
        }
    }
}
