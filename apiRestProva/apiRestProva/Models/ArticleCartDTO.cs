using apiRestProva.Entities;
using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Models
{
    public class ArticleCartDTO
    {
        
        public string ArticleCode { get; set; }
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

    }
}
