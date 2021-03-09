using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCondominios.BLL.Models
{
    public class Pagamento
    {
        [Key]
        public int Codigo { get; set; }

        public string CodigoUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int CodigoAluguel { get; set; }

        public virtual Aluguel Aluguel { get; set; }

        public DateTime? DataPagamento { get; set; }

        public StatusPagamento Status { get; set; }
    }

    public enum StatusPagamento
    {
        Pago, Pendente, Atrasado
    }
}
