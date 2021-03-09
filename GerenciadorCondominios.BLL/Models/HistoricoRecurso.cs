using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class HistoricoRecurso
    {
        public int Codigo { get; set; }

        public decimal Valor { get; set; }

        public Tipos Tipo { get; set; }

        public int Dia { get; set; }

        public int CodigoMes { get; set; }

        public virtual Mes Mes { get; set; }

        public int Ano { get; set; }
    }

    public enum Tipos
    {
        Entrada, Saida
    }
}
