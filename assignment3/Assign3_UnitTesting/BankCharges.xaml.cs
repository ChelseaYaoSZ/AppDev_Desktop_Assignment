using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Assign3_UnitTesting.Models;

namespace Assign3_UnitTesting
{
    /// <summary>
    /// Interaction logic for BankCharges.xaml
    /// </summary>
    public partial class BankCharges : Window
    {
        public BankCharges()
        {
            InitializeComponent();
        }

        public void calcMonthlyServFee_Click(object sender, RoutedEventArgs e) //needed to change to public to do unit testing
        {
            int noCheques = int.Parse(noOfCheques.Text);
            double endBalance = double.Parse(endingBalance.Text);

            var bankChargesCalculator = new BankCharge();
            double totalBankFee = bankChargesCalculator.CalculateTotalBankFee(noCheques, endBalance);

            MessageBox.Show("Bank service fee: " + totalBankFee + "$");
        }
    }
    
}
