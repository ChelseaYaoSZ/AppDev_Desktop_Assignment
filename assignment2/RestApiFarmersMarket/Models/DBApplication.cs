using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace RestApiFarmersMarket.Models
{
    public class DBApplication
    {
        //GetAllProducts Method
        public Response GetAllProducts(NpgsqlConnection con) 
        {
            string Query = "Select * from products";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Response response = new Response();

            List<Product> products = new List<Product>();

            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Product product = new Product();
                    product.product_id = (int)dt.Rows[i]["product_id"];
                    product.product_name = (string)dt.Rows[i]["product_name"];
                    product.amount = (double)dt.Rows[i]["amount"];
                    product.price = (double)dt.Rows[i]["price"];

                    products.Add(product);
                }
            }

            if(products.Count > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Data Retrieved Successfully";
                response.product = null;
                response.products = products;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data failed to retrieve or table is empty";
                response.product = null;
                response.products = null;
            }

            return response;
        }

        //Get product by id method
        public Response GetProductById(NpgsqlConnection con, int id)
        {
            Response response = new Response();
            string Query = "Select * from products where product_id='"+ id +"'"; //inline param with query
           
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                Product product = new Product();
                product.product_id = (int)dt.Rows[0]["product_id"];
                product.product_name = (string)dt.Rows[0]["product_name"];
                product.amount = (double)dt.Rows[0]["amount"];
                product.price = (double)dt.Rows[0]["price"];

                response.statusCode = 200;
                response.messageCode = "Product Successfully Retrieved";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data couldn't be found...check the id";
                response.product = null;
                response.products = null;
            }

            return response;

        }

        //Get product by name method
        public Response GetProductByName(NpgsqlConnection con, string name)
        {
            Response response = new Response();
            string Query = "Select * from products where product_name='" + name + "'"; //inline param with query

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Product product = new Product();
                product.product_id = (int)dt.Rows[0]["product_id"];
                product.product_name = (string)dt.Rows[0]["product_name"];
                product.amount = (double)dt.Rows[0]["amount"];
                product.price = (double)dt.Rows[0]["price"];

                response.statusCode = 200;
                response.messageCode = "Product Successfully Retrieved";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data couldn't be found...check the id";
                response.product = null;
                response.products = null;
            }

            return response;

        }

        //Add Product Method
        public Response AddProduct(NpgsqlConnection con, Product product)
        {
            con.Open();
            Response response = new Response();
            string Query = "insert into products values(@product_id, @product_name, @amount, @price)";
           
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@product_id", product.product_id);
            cmd.Parameters.AddWithValue("@product_name", product.product_name);
            cmd.Parameters.AddWithValue("@amount", product.amount);
            cmd.Parameters.AddWithValue("@price", product.price);

            int i = cmd.ExecuteNonQuery();

            if(i>0)
            {
                response.statusCode=200;
                response.messageCode = "Successfully Inserted";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Insertion is not successful";
                response.product = null;
                response.products = null;
            }
            con.Close();
            return response;
        }

        //Update Product Information
       public Response UpdateProduct(NpgsqlConnection con, Product product)
        {
            con.Open();
            Response response = new Response();
            string Query = "Update products set product_name=@product_name, amount=@amount, price=@price where product_id=@ID";
            
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
           
            cmd.Parameters.AddWithValue("@product_name", product.product_name);
            cmd.Parameters.AddWithValue("@amount", product.amount);
            cmd.Parameters.AddWithValue("@price", product.price);
            cmd.Parameters.AddWithValue("@ID", product.product_id);

            int i = cmd.ExecuteNonQuery();

            if (i>0)
            {
                response.statusCode=200;
                response.messageCode = "Updated Successfully";
                response.product = product;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Update failed or id wasn't in correct form";
            }

            con.Close();
            return response;
        }
        
       //Delete Product from DB by ID
       public Response DeleteProductById(NpgsqlConnection con, int id)
        {
            con.Open();
            Response response = new Response();
            string Query = "Delete from products where product_id='" + id + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            int i = cmd.ExecuteNonQuery();

            if(i>0) 
            {
                response.statusCode=200;
                response.messageCode = "Product Deleted Successfully";
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Product not found! Couldn't perform delete operation";            
            }
            con.Close();
            return response;

        }

    }
}
