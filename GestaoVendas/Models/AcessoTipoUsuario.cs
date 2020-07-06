using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("AcessoTipoUsuario")]
    public class AcessoTipoUsuario
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string NomeFuncionalidade { get; set; }

        [Display(Name = "Tipo Usuário")]
        [ForeignKey("TipoUsuario")]
        [Column(Order = 1)]
        public int IdTipoUsuario { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
