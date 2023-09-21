using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Entities
{
    public class AuthToken
    {
        [Key]
        public string AccessToken { get; set; }
        public bool validity { get; set; }
    }
}
