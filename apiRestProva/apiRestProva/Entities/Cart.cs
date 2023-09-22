using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apiRestProva.Models;

namespace apiRestProva.Entities
{
    public class Cart
    {
        public List<ArticleCart> articles { get; set; }
        public DateTime expireCartDatetime { get; set; }
        [Key]
        [ForeignKey("ArticleCart")]
        public string cartId { get; set; }
        public decimal totalAmount { get; set; }

        public CartDTO MapToDTO()
        {
            var cartDTO = new CartDTO();
            var articleDTO = new List<ArticleCartDTO>();
            foreach (var ac in articles)
            {
                articleDTO.Add(ac.MapToDTO());
            }
            cartDTO.expireCartDatetime = expireCartDatetime;
            cartDTO.cartId = cartId;
            cartDTO.articles = articleDTO;
            return cartDTO;
        }
        public PreviewDTO MapToPreview()
        {
            var previewDTO = new PreviewDTO();
            var articleDTO = new List<ArticleCartDTO>();
            foreach (var ac in articles)
            {
                articleDTO.Add(ac.MapToDTO());
            }
            previewDTO.expireCartDatetime = expireCartDatetime;
            previewDTO.cartId = cartId;
            previewDTO.articles = articleDTO;
            return previewDTO;
        }

    }

}