using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GestaoVendas.Models
{
    [Table("PerfilUsuario")]
    public class PerfilUsuario
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "TipoUsuario")]
        [ForeignKey("TipoUsuario")]
        [Column(Order = 1)]
        public int IdTipoUsuario { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }


        [Display(Name = "Usuário")]
        [ForeignKey("ItentityUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual IdentityUser ItentityUser { get; set; }
    }
}
