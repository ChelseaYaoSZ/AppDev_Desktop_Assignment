using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign3_UnitTesting.Models
{
    public class BankCharge
    {
        public double CalculateTotalBankFee(int noCheques, double endBalance)
        {
            double bankCharge = 10.0;

            if (endBalance < 400)
            {
                bankCharge += 15;
            }

            double feePerCheque = 0.10; 

            if (noCheques >= 20 && noCheques < 40)
            {
                feePerCheque = 0.08;
            }
            else if (noCheques >= 40 && noCheques < 60)
            {
                feePerCheque = 0.06;
            }
            else if (noCheques >= 60)
            {
                feePerCheque = 0.10;
            }

            double totalChequeFee = feePerCheque * noCheques;
            double totalBankFee = totalChequeFee + bankCharge;
            return totalBankFee;
        }
    }
}
