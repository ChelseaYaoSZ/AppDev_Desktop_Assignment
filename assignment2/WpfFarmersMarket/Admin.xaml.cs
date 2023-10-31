using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        HttpClient client = new HttpClient();
        public Admin()
        {
            client.BaseAddress = new Uri("https://localhost:7205/Product/");

            //Step 02: we need to clear the default network packet header informaiton
            client.DefaultRequestHeaders.Accept.Clear();

            // Step 03: Add our packet information to the http request
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")

                );
            InitializeComponent();
        }

        private async void showAll_Click(object sender, RoutedEventArgs e)
        {
            //Step 01: Create/ Call the method to get all the products
            var server_response = await client.GetStringAsync("GetAllProducts");
            
            // step 02: Decrypt/extract the server_response
            Response response_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            dataGrid.ItemsSource = response_JSON.products;
            DataContext = this;
        }

        private async void select_Click(object sender, RoutedEventArgs e)
        {
            // Step 01: Create/ Call the method to search out one student from the database
            var server_response =
                await client.GetStringAsync("GetProductbyId/" + int.Parse(search.Text));

            Response response_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            p_name.Text = response_JSON.product.product_name;
            p_id.Text = response_JSON.product.product_id.ToString();
            amount.Text = response_JSON.product.amount.ToString();
            price.Text = response_JSON.product.price.ToString();
        }

        private async void insert_Click(object sender, RoutedEventArgs e)
        {
            
            Product product = new Product();

            product.product_name = p_name.Text;
            product.amount = double.Parse(amount.Text);
            product.price = double.Parse(price.Text);

            var server_response =
                await client.PostAsJsonAsync("AddProduct", product);

            MessageBox.Show(server_response.ToString());
        }

        private async void update_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.product_name = p_name.Text;
            product.amount = double.Parse(amount.Text);
            product.price = double.Parse(price.Text);
            product.product_id = int.Parse(p_id.Text);

            var server_response =
                await client.PutAsJsonAsync("UpdateProduct", product);
            MessageBox.Show(server_response.ToString());
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            var response_JSON =
                await client.DeleteAsync("DeleteProductbyId/"
                + int.Parse(search.Text));

            MessageBox.Show(response_JSON.StatusCode.ToString());
            MessageBox.Show(response_JSON.RequestMessage.ToString());
        }
    }
}
