using apiRestProva.Entities;

namespace apiRestProva.Models
{
    public class ArticleCartDTO
    {
        public string ArticleCode { get; set; }
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

    }
}
