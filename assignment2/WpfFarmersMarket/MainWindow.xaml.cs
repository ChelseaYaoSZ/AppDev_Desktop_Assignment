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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarmersMarket
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

        private void select_Click(object sender, RoutedEventArgs e)
        {

            if (admin_button.IsChecked == true)
            {
                Admin admin = new Admin();
                admin.Show(); //this will activate the wpf of the class you created an instance of
                              //this.Close(); if you want to close the current window
            }


            if (sales_button.IsChecked == true)
            {
                Sales sales = new Sales();
                sales.Show();
            }


        }
    }
}


