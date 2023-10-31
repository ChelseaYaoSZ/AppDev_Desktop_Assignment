using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WpfFarmersMarket.Modules;

namespace WpfFarmersMarket
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        HttpClient client = new HttpClient();

        private const double APPLE_PRICE = 2.10;
        private const double ORANGE_PRICE = 2.49;
        private const double RASPBERRY_PRICE = 2.35;
        private const double BLUEBERRY_PRICE = 1.45;
        private const double CAULIFLOWER_PRICE = 2.22;

        public Sales()
        {
            client.BaseAddress = new Uri("https://localhost:7205/Product/");

            //Step 02: we need to clear the default network packet header informaiton
            client.DefaultRequestHeaders.Accept.Clear();

            // Step 03: Add our packet information to the http request
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")

                );
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
            totalSubtotal = 0.0; // Reset the total subtotal

            foreach (TextBox textBox in new List<TextBox>
            {
                apple_amount, orange_amount, raspberry_amount, blueberry_amount, cauliflower_amount
            })
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

                    // Update the total subtotal
                    totalSubtotal += (amount * price);
                }
                else
                {
                    targetTextBlock.Text = "0.00";
                }
            }

            // Update the total_subtotal TextBlock
            total_subtotal.Text = $"Total Subtotal: ${totalSubtotal:F2}";
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

        private async void UpdateInventory(Dictionary<string, double> productPurchases)
        {
            try
            {
                foreach (KeyValuePair<string, double> keyValuePair in productPurchases)
                {
                    // Call GetProductByName API
                    string ProductName = keyValuePair.Key;
                    double PurchaseAmount = keyValuePair.Value;

                    var server_response_1 =
                        await client.GetStringAsync("GetProductbyName/" + ProductName);

                    Response response_JSON = JsonConvert.DeserializeObject<Response>(server_response_1);

                    int ProductId = int.Parse(response_JSON.product.product_id.ToString());
                    double Amount = double.Parse(response_JSON.product.amount.ToString()) - PurchaseAmount;
                    double Price = double.Parse(response_JSON.product.price.ToString());


                    Product product = new Product();
                    product.product_name = ProductName;
                    product.amount = Amount;
                    product.price = Price;
                    product.product_id = ProductId;

                    var server_response_2 =
                        await client.PutAsJsonAsync("UpdateProduct", product);

                    if (!server_response_2.IsSuccessStatusCode)
                    {
                        // Handle errors here
                        MessageBox.Show("Failed to update inventory. Error: " + server_response_2.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            
        }

        private double GetSubtotalFromTextBlock(TextBlock textBlock)
        {
            return double.TryParse(textBlock.Text, out double value) ? value : 0;
        }
    }
}
