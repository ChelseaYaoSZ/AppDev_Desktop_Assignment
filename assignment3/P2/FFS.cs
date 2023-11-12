using System;

namespace P2
{
    internal class FFS
    {
        public double CalculateShippingFee(double weight, double distance)
        {
            double baseFee = 0.0;
            double distanceFactor = Math.Ceiling(distance / 500);

            if (weight <= 2.0)
            {
                baseFee = 1.1;
            }
            else if (2.0 < weight && weight <= 6.0)
            {
                baseFee = 2.2;
            }
            else if (6.0 < weight && weight <= 10.0)
            {
                baseFee = 3.7;
            }
            else
            {
                baseFee = 4.8;
            }

            return distanceFactor * baseFee;
        }
    }
}

