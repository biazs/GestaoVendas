using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("AcessoTipoUsuario")]
    public class AcessoTipoUsuario
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo Usuário")]
        [ForeignKey("TipoUsuario")]
        [Column(Order = 1)]
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Funcionalidade")]
        [ForeignKey("Funcionalidade")]
        [Column(Order = 1)]
        public int IdFuncionalidade { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        public virtual Funcionalidade Funcionalidade { get; set; }
    }
}
