namespace RestApiFarmersMarket.Models
{
    public class Response
    {
        public int statusCode {  get; set; }
        public string messageCode { get; set; }
        public Product product { get; set; }
        public List<Product> products { get; set; }
    }
}
