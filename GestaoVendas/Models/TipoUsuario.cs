using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("TipoUsuario")]
    public class TipoUsuario
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo Usuário")]
        public string NomeTipoUsuario { get; set; }
    }
}
