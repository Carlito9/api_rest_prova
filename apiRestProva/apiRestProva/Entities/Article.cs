using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Entities
{
    public class Article
    {
        [Key]
        public string ArticleCode { get; set; }
        public string ArticleDescription { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public decimal Price { get; set; }
        public int MaxQuantity { get; set; }

    }
}