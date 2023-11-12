using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{

    public class ShippingFeeCalculatorTests
    {
        [TestCase(1.0, 500, ExpectedResult = 1.1)]
        [TestCase(3.0, 1000, ExpectedResult = 4.4)]
        [TestCase(11.0, 2000, ExpectedResult = 19.2)]
        public double CalculateShippingFee_ShouldCalculateCorrectly(double weight, double distance)
        {           
            var calculator = new FFS(); 

            var result = calculator.CalculateShippingFee(weight, distance);

            return result;
        }
    }
}
