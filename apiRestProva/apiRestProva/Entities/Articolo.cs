using System.ComponentModel.DataAnnotations;

namespace apiRestProva.Entities
{
    public class Articolo
    {
        [Key]
        public string codiceArticolo { get; set; }
        public string descrizioneArticolo { get; set; }
        public int validita { get; set; }
        public double prezzo { get; set; }
        public int idCatalogoArticolo { get; set; }

    }
}