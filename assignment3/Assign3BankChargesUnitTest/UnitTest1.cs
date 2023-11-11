using Assign3_UnitTesting;
using Assign3_UnitTesting.Models;
using NUnit.Framework;

namespace Assign3BankChargesUnitTest
{
    public class Tests
    {
        private BankCharge bankCharges { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            bankCharges = new BankCharge();
        }

        // Less than 20 checks
        [Test]
        public void CalculateTotalBankFee_WithLessThan20ChecksAndHighBalance_ShouldApplyBaseRate()
        {
            //Arrange
            int noCheques = 15;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.10 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithLessThan20ChecksAndLowBalance_ShouldApplyBaseRateAndExtraFee()
        {
            //Arrange
            int noCheques = 15;
            double endBalance = 300;
            double expectedFeeResults = 25 + (0.10 * noCheques); // $10 base fee + $15 low balance fee + $0.10 per check

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        // Between 20 and 39 checks
        [Test]
        public void CalculateTotalBankFee_With20To39ChecksAndHighBalance_ShouldApplyLowerCheckRate()
        {
            //Arrange
            int noCheques = 25;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.08 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);
           
            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_With20To39ChecksAndLowBalance_ShouldApplyLowerCheckRateAndExtraFee()
        {
            //Arrange
            int noCheques = 25;
            double endBalance = 350;
            double expectedFeeResults = 25 + (0.08 * noCheques); // $10 base fee + $15 low balance fee + $0.08 per check

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);
            
            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        // Between 40 and 59 checks
        [Test]
        public void CalculateTotalBankFee_With40To59ChecksAndHighBalance_ShouldApplyEvenLowerCheckRate()
        {
            //Arrange
            int noCheques = 45;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.06 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }
        [Test]
        public void CalculateTotalBankFee_With40To59ChecksAndLowBalance_ShouldApplyEvenLowerCheckRateAndExtraFee()
        {
            //Arrange
            int noCheques = 45;
            double endBalance = 350;
            double expectedFeeResults = 25 + (0.06 * noCheques); // $10 base fee + $15 low balance fee + $0.06 per check

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        // More than 60 checks
        [Test]
        public void CalculateTotalBankFee_WithMoreThan60ChecksAndHighBalance_ShouldApplyBaseRate()
        {
            //Arrange
            int noCheques = 65;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.10 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithMoreThan60ChecksAndLowBalance_ShouldApplyBaseRateAndExtraFee()
        {
            //Arrange
            int noCheques = 65;
            double endBalance = 300;
            double expectedFeeResults = 25 + (0.10 * noCheques); // $10 base fee + $15 low balance fee + $0.10 per check

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        // Boundary conditions
        [Test]
        public void CalculateTotalBankFee_WithExactly20ChecksAndBalanceAbove400()
        {
            //Arrange
            int noCheques = 20;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.08 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithExactly40ChecksAndBalanceAbove400()
        {
            //Arrange
            int noCheques = 40;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.06 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithExactly60ChecksAndBalanceAbove400()
        {
            //Arrange
            int noCheques = 60;
            double endBalance = 500;
            double expectedFeeResults = 10 + (0.10 * noCheques); 

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        //Edge Case
        [Test]
        public void CalculateTotalBankFee_With20To39ChecksAndExactlyBalance400()
        {
            //Arrange
            int noCheques = 30;
            double endBalance = 400;
            double expectedFeeResults = 10 + (0.08 * noCheques); 

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_With40To59ChecksAndExactlyBalance400()
        {
            //Arrange
            int noCheques = 50;
            double endBalance = 400;
            double expectedFeeResults = 10 + (0.06 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithOver60ChecksAndExactlyBalance400()
        {
            //Arrange
            int noCheques = 70;
            double endBalance = 400;
            double expectedFeeResults = 10 + (0.10 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

        [Test]
        public void CalculateTotalBankFee_WithUnder20ChecksAndExactlyBalance400()
        {
            //Arrange
            int noCheques = 15;
            double endBalance = 400;
            double expectedFeeResults = 10 + (0.10 * noCheques);

            //Act
            double totalBankFeeResults = bankCharges.CalculateTotalBankFee(noCheques, endBalance);

            //Assert
            Assert.AreEqual(expectedFeeResults, totalBankFeeResults);

            //Datatype Testing
            Assert.That(totalBankFeeResults, Is.TypeOf<double>());
        }

    }
}