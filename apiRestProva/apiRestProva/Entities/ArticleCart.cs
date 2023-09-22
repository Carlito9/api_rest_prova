using apiRestProva.Models;
using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Entities
{
    public class ArticleCart
    {
        [Key]
        public string ArticleCode { get; set; }
        [Key]
        public string CartId { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ArticleCartDTO MapToDTO()
        {
            ArticleCartDTO articleCartDTO = new ArticleCartDTO();

            articleCartDTO.ArticleCode = ArticleCode;
            articleCartDTO.Price = Price;
            articleCartDTO.Quantity = Quantity;
            return articleCartDTO;

        }
    }
}
