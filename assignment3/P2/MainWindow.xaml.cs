using System.Windows;

namespace P2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double packageWeight = double.Parse(weight.Text);
            int shipDistance = int.Parse(distance.Text);

            FFS newShipping = new FFS();
            double fees = newShipping.CalculateShippingFee(packageWeight, shipDistance);
            MessageBox.Show("The total charge for your package is: " + fees + "$");
        }
    }
}
