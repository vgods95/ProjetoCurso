using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Veiculo
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "Tamanho máximo atingido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "Tamanho máximo atingido")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "Tamanho máximo atingido")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Placa { get; set; }
        public string CodigoUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
