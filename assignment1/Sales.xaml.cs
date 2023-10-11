using Npgsql;
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

namespace assignment1
{
    public partial class sales : Window
    {
        // Prices for each product
        private const double APPLE_PRICE = 2.10;
        private const double ORANGE_PRICE = 2.49;
        private const double RASPBERRY_PRICE = 2.35;
        private const double BLUEBERRY_PRICE = 1.45;
        private const double CAULIFLOWER_PRICE = 2.22;

        public sales()
        {
            InitializeComponent();

            // Attach TextChanged event handlers to the TextBoxes
            apple_amount.TextChanged += ProductAmount_TextChanged;
            orange_amount.TextChanged += ProductAmount_TextChanged;
            raspberry_amount.TextChanged += ProductAmount_TextChanged;
            blueberry_amount.TextChanged += ProductAmount_TextChanged;
            cauliflower_amount.TextChanged += ProductAmount_TextChanged;

            purchase.Click += Purchase_Click;
        }

        private double totalSubtotal = 0.0;

        private void ProductAmount_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                double price = 0;
                TextBlock targetTextBlock = null;
              
                switch (textBox.Name)
                {
                    case "apple_amount":
                        price = APPLE_PRICE;
                        targetTextBlock = apple_subtotal;
                        break;
                    case "orange_amount":
                        price = ORANGE_PRICE;
                        targetTextBlock = orange_subtotal;
                        break;
                    case "raspberry_amount":
                        price = RASPBERRY_PRICE;
                        targetTextBlock = raspberry_subtotal;
                        break;
                    case "blueberry_amount":
                        price = BLUEBERRY_PRICE;
                        targetTextBlock = blueberry_subtotal;
                        break;
                    case "cauliflower_amount":
                        price = CAULIFLOWER_PRICE;
                        targetTextBlock = cauliflower_subtotal;
                        break;
                }

                if (double.TryParse(textBox.Text, out double amount))
                {
                    targetTextBlock.Text = (amount * price).ToString("F2");

                    //update the total subtotal
                    double subtotal = price * amount;
                    totalSubtotal += subtotal;
                    total_subtotal.Text = $"Total Subtotal: ${totalSubtotal:F2}";
                }
                else
                {
                    targetTextBlock.Text = "0.00";
                }
            }
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            double total = GetSubtotalFromTextBlock(apple_subtotal) +
                           GetSubtotalFromTextBlock(orange_subtotal) +
                           GetSubtotalFromTextBlock(raspberry_subtotal) +
                           GetSubtotalFromTextBlock(blueberry_subtotal) +
                           GetSubtotalFromTextBlock(cauliflower_subtotal);


            MessageBox.Show($"Total purchase amount: ${total:F2}", "Purchase", MessageBoxButton.OK, MessageBoxImage.Information);

            // Create a dictionary to store products and the amounts client wants
            Dictionary<string, double> productPurchases = new Dictionary<string, double>();

            // Check if apple textbox has a value, and if it's more than 0
            if (!string.IsNullOrWhiteSpace(apple_amount.Text) && double.Parse(apple_amount.Text) > 0)
                productPurchases.Add("Apple", double.Parse(apple_amount.Text));

            // Similar checks for other products
            if (!string.IsNullOrWhiteSpace(raspberry_amount.Text) && double.Parse(raspberry_amount.Text) > 0)
                productPurchases.Add("Raspberry", double.Parse(raspberry_amount.Text));

            // ... Do the same for other products
            if (!string.IsNullOrWhiteSpace(orange_amount.Text) && double.Parse(orange_amount.Text) > 0)
                productPurchases.Add("Orange", double.Parse(orange_amount.Text));

            if (!string.IsNullOrWhiteSpace(cauliflower_amount.Text) && double.Parse(cauliflower_amount.Text) > 0)
                productPurchases.Add("Cauliflower", double.Parse(cauliflower_amount.Text));

            if (!string.IsNullOrWhiteSpace(blueberry_amount.Text) && double.Parse(blueberry_amount.Text) > 0)
                productPurchases.Add("Blueberry", double.Parse(blueberry_amount.Text));

            // Update the inventory for the products client wants
            UpdateInventory(productPurchases);
        }

        private void UpdateInventory(Dictionary<string, double> productPurchases)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    con.Open();

                    foreach (KeyValuePair<string, double> product in productPurchases)
                    {
                        string query = "UPDATE products SET amount = amount - @Amount WHERE product_name = @ProductName";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Amount", product.Value);
                            cmd.Parameters.AddWithValue("@ProductName", product.Key);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private double GetSubtotalFromTextBlock(TextBlock textBlock)
        {
            return double.TryParse(textBlock.Text, out double value) ? value : 0;
        }
    }
}

