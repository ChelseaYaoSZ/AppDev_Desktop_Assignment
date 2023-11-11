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
    /// Interaction logic for PopulationPrediction.xaml
    /// </summary>
    public partial class PopulationPrediction : Window
    {
        public PopulationPrediction()
        {
            InitializeComponent();
        }
        private void Population_Growth_Click(object sender, RoutedEventArgs e)
        {
            int popSize;

            if (int.Parse(populationSize.Text) < 2)
            {
                MessageBox.Show("Invalid Population, please enter a 2 or higher");
                return;
            }
            else
            {
                popSize = int.Parse(populationSize.Text);
            }

            double incrRate;

            if (double.Parse(increaseRate.Text) < 0)
            {
                MessageBox.Show("Invalid input, cannot be a negative number");
                return;
            }
            else
            {
                incrRate = double.Parse(increaseRate.Text) / 100;  //will be converted percentage
            }

            int noDays = int.Parse(comboBox1.Text);

            //need for loop to go from day 1 population to noDays population
            int daysPop = popSize;
            for (int i = 1; i <= noDays; i++)
            {
                double increasePopulation = daysPop * incrRate;
                daysPop = (int)(daysPop + increasePopulation);
                MessageBox.Show("Day no: " + i + " & predicted population: " + daysPop);
            }
        }
    }
}
