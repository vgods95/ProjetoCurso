using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Servico
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public decimal Valor { get; set; }

        public StatusServico Status { get; set; }

        public string CodigoUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        public ICollection<ServicoPredio> ServicosPredios { get; set; }
    }

    public enum StatusServico
    {
        Pendente, Recusado, Aceito
    }
}
