namespace apiRestProva.Models
{
    public class ArticleDTO
    {
        public string articleCode { get; set; }
        public string articleDescription { get; set; }
        public string carrierCode { get; set; }
        public string carrierName { get; set; }
        public decimal price { get; set;}
        public int maxQuantity { get; set; }


    }
}
