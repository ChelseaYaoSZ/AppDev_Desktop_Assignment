using Microsoft.AspNetCore.Mvc;
using Npgsql;
using RestApiFarmersMarket.Models;

namespace RestApiFarmersMarket.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //GetAllProducts API
        [HttpGet]
        [Route("GetAllProducts")]

        //Create API Method
        public Response GetAllProducts()
        {
            Response response = new Response();

            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dBA = new DBApplication();
            response = dBA.GetAllProducts(con);

            return response;
        }

        //Search 1 product by ID
        [HttpGet]
        [Route("GetProductById/{id}")]

        public Response GetProductById(int id)
        { 
            Response response = new Response();
            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));
            DBApplication dBA = new DBApplication();
            response = dBA.GetProductById(con, id);
            return response;
        }

        //insert a new product
        [HttpPost]
        [Route("AddProduct")]

        public Response AddProduct(Product product)
        {
            Response response = new Response();
            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));
            DBApplication dBA = new DBApplication();
            dBA.AddProduct(con, product);
            return response;
        }

        //Update Product Info
        [HttpPut]
        [Route("UpdateProduct")]

        public Response UpdateProduct(Product product)
        {
            Response response = new Response();
            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));
            DBApplication dBA = new DBApplication();
            dBA.UpdateProduct(con, product);
            return response;
        }

        //Delete Product Info
        [HttpDelete]
        [Route("DeleteProductById/{id}")]

        public Response DeleteProductById(int id)
        {
            Response response = new Response();
            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));
            DBApplication dBA = new DBApplication();
            response = dBA.DeleteProductById(con, id);
            return response;
        }


    }
}
