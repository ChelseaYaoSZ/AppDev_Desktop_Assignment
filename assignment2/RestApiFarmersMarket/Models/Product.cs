namespace RestApiFarmersMarket.Models
{
    public class Product
    {
        //This class is made to get all the info properly from DB 
        //that is why we need to have the same number of variables as DB
        //table, it's better to maintain the sequence and names as well

        public int product_id { get; set; }

        public string product_name { get; set; }

        public double amount { get; set; }

        public double price { get; set; }

    }
}
