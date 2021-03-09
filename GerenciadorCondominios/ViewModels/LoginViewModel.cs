using System.ComponentModel.DataAnnotations;

namespace GerenciadorCondominios.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage ="E-mail inválido!")]
        [Required(ErrorMessage= "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }
    }
}
