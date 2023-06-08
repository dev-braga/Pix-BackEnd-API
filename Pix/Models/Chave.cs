namespace Pix.Models
{
    public class Chave
    {
        public int Id { get; set; } 
        public string ChavePix { get; set; }
        public int ClienteId { get; set;  }
        public string Tipo { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
