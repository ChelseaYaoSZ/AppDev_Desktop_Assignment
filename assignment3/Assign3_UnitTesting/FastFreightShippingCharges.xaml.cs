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

namespace Assign3_UnitTesting
{
    /// <summary>
    /// Interaction logic for FastFreightShippingCharges.xaml
    /// </summary>
    public partial class FastFreightShippingCharges : Window
    {
        public FastFreightShippingCharges()
        {
            InitializeComponent();
        }

        private void calcShipCharge_Click(object sender, RoutedEventArgs e)
        {
            double packageWeight = double.Parse(weightOfPackage.Text);
            int shipDistance = int.Parse(shippingDistance.Text);
            double shippingRate = 0;
            double shippingCharges = 0;

            if (packageWeight <= 2.0)
            {
                if (shipDistance <= 500)
                {
                    shippingRate = 1.10;
                }
                else if (shipDistance > 500 && shipDistance <= 1000)
                {
                    shippingRate = 2.20;
                }
                else if (shipDistance > 1000 && shipDistance <= 1500)
                {
                    shippingRate = 3.30;
                }
                else if (shipDistance > 1500 && shipDistance <= 2000)
                {
                    shippingRate = 4.40;
                }
                else
                {
                    shippingRate = 5.00;
                }
            }
            else if (packageWeight <= 6.0)
            {
                if (shipDistance <= 500)
                {
                    shippingRate = 2.20;
                }
                else if (shipDistance > 500 && shipDistance <= 1000)
                {
                    shippingRate = 4.40;
                }
                else if (shipDistance > 1000 && shipDistance <= 1500)
                {
                    shippingRate = 6.60;
                }
                else if (shipDistance > 1500 && shipDistance <= 2000)
                {
                    shippingRate = 8.80;
                }
                else
                {
                    shippingRate = 10.00;
                }
            }
            else if (packageWeight <= 10)
            {
                if (shipDistance <= 500)
                {
                    shippingRate = 3.70;
                }
                else if (shipDistance > 500 && shipDistance <= 1000)
                {
                    shippingRate = 7.40;
                }
                else if (shipDistance > 1000 && shipDistance <= 1500)
                {
                    shippingRate = 11.10;
                }
                else if (shipDistance > 1500 && shipDistance <= 2000)
                {
                    shippingRate = 14.80;
                }
                else
                {
                    shippingRate = 15.00;
                }
            }
            else
            {
                if (shipDistance <= 500)
                {
                    shippingRate = 4.80;
                }
                else if (shipDistance > 500 && shipDistance <= 1000)
                {
                    shippingRate = 9.60;
                }
                else if (shipDistance > 1000 && shipDistance <= 1500)
                {
                    shippingRate = 14.40;
                }
                else if (shipDistance > 1500 && shipDistance <= 2000)
                {
                    shippingRate = 19.20;
                }
                else
                {
                    shippingRate = 20.00;
                }
            }

            shippingCharges = shippingRate;
            MessageBox.Show("The total charge for your package is: " + shippingCharges.ToString("F2") + "$");
        }
    }
}
