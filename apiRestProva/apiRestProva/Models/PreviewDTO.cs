using apiRestProva.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Models
{
    public class PreviewDTO
    {
        public List<ArticleCartDTO> articles { get; set; }
        public DateTime expireCartDatetime { get; set; }
        
        public string cartId { get; set; }
        public decimal totalAmount { get; set; }

    }
}
