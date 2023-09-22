

namespace apiRestProva.Models
{
    public class CartDTO
    {
        public List<ArticleCartDTO> articles { get; set; }
        public DateTime expireCartDatetime { get; set; }
        public string cartId { get; set; }
    }
}
