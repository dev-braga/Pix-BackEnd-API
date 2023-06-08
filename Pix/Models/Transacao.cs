using Pix.DTO;

namespace Pix.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataHora { get; set; }
        public int ChaveId { get; set; }
        public int ClienteId { get; set; }
        public double Valor { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Chave Chave { get; set; }
    }
}
