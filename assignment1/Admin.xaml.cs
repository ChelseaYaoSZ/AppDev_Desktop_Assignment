using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using System.Data;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace assignment1
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        public static NpgsqlConnection con;

        public static NpgsqlCommand cmd;

        private void establishConnect()
        {
            try
            {
                con = new NpgsqlConnection(get_ConnectionString());
                MessageBox.Show("Connection Established");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string get_ConnectionString()
        {
            string host = "Host=localhost;";
            string port = "Port=5432;";
            string dbName = "Database=farmers_market;";
            string userName = "Username=postgres;";
            string password = "Password=133080;";

            string connectionString = string.Format("{0}{1}{2}{3}{4}", host, port, dbName, userName, password);
            return connectionString;

        }

        private void showAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnect();

                //Step 2: Open connection
                con.Open();

                //Step 3: Create the Query
                string Query = "select * from products";

                //Step 4: Initialeze the command Adapter with connection
                cmd = new NpgsqlCommand(Query, con);

                //Step 5: We need to create a SQL dataAdapter and a SQL dataTable, 
                //read the data at time of executing the select command
                //and set it in the dataTable and then push it to the 
                //dataAdapter to set it back to DataGrid
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //Step 6: Send the dataTable information to dataGrid itemSource
                dataGrid.ItemsSource = dt.AsDataView(); //this line is going to copy the whole
                                                        //dataTable it gets from the adapter to the dataGrid view
                                                        //and with AsDataView(), we are ensuring that, dataGrid is getting the full table data Itself
                                                        //as it is.

                //Step 7: Reinitialize our wpf controls data, specially for dataGrid
                DataContext = da;

                //Step 8: close connection
                con.Close();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // step 1: establish connection
                establishConnect();
                // step 2: OPen the connection
                con.Open();
                // step 3: create the Query
                string Query = "select * from products where product_id=@Id";
                // step 4: initialize the command Adapter with connection & Query
                cmd = new NpgsqlCommand(Query, con);
                //step 4.1: need to initialize the query variable
                cmd.Parameters.AddWithValue("@Id", int.Parse(search.Text));
                //step 4.2 put a checker/boolean to see whether the data is present or not
                bool noData = true;
                // step 5: Data Reader adapter
                NpgsqlDataReader dr = cmd.ExecuteReader();// this line is going to 
                                                          // read all the data matches with the query and return a list of database
                                                          // entries
                                                          // step 6: checking all the pulled up entries from the database one by one
                while (dr.Read())
                {
                    noData = false;
                    p_name.Text = dr["product_name"].ToString();
                    p_id.Text = dr["product_id"].ToString();
                    amount.Text = dr["amount"].ToString();
                    price.Text = dr["price"].ToString();                 
                }
                if (noData)// checking the data Retrival
                {
                    MessageBox.Show("There is no product with this id in the database.");
                }
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // step 1: Establish Connection
                establishConnect();
                // step 2: open the connection
                con.Open();
                // step 3: Generating the query
                string Query = "insert into products values(@name, default, @amount, @price)";
                // step 4: Pass the query to Command adapter
                cmd = new NpgsqlCommand(Query, con); // dynamic memory allocation of the command

                // step 4.1 : add/define values for the variables in the query
                cmd.Parameters.AddWithValue("@name", p_name.Text);
                cmd.Parameters.AddWithValue("@amount", double.Parse(amount.Text));
                cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));
           
                /*
                 * We always need to focus on the data type the destination database has, for each 
                 * column. Here, registration year column has int datatype. SO, you need to convert 
                 * the year.Text string format data to the integer one.
                 */
                //step 5: Execute the Command
                cmd.ExecuteNonQuery();
                // step 6: send/set a successful message
                MessageBox.Show("Products Created Successfully");
                // step 7: close the connection
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // step 1: establish the connection
                establishConnect();
                // step 2: Open the connection
                con.Open();
                // step 3: Generate the query
                string Query = "delete from products where product_id=@ID";
                // step 4: initialize the command adapter
                cmd = new NpgsqlCommand(Query, con);
                // step 4.1 initialize the parameter
                cmd.Parameters.AddWithValue("@ID", int.Parse(search.Text));
                // step 5: execute the query
                cmd.ExecuteNonQuery();
                // step 6: success message
                MessageBox.Show("Delete Successful");
                // step 7: Closing the connection
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //step 1: establishment connection
                establishConnect();
                //step 2: open the connection
                con.Open();
                // step 3: Query Generation
                string Query = "Update products set product_name=@name, amount=@amount, price=@price where product_id=@ID";
                // step 4: initialize the command adapter of the database
                cmd = new NpgsqlCommand(Query, con);
                // step 4.1: initialize the parameters in the variables of command 
                cmd.Parameters.AddWithValue("@name", p_name.Text);
                cmd.Parameters.AddWithValue("@amount", double.Parse(amount.Text));
                cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));
                cmd.Parameters.AddWithValue("@ID", int.Parse(search.Text));

                // step 5: Executing the query
                cmd.ExecuteNonQuery();
                // step 6: Successful Message
                MessageBox.Show("Update successful");

                // step 7: Close the connection
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
