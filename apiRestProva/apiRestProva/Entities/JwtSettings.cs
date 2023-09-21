namespace apiRestProva.Entities
{
    public class JwtSettings
    {
        
        public string SecurityKey { get; set; }

        //chi emette il token
        public string Issuer { get; set; }

        //a chi è destinato il token
        public string Audience { get; set; }

    }
}
