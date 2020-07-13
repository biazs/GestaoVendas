using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Funcionalidade")]
    public class Funcionalidade
    {
        [Display(Name = "Código")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string NomeFuncionalidade { get; set; }

    }
}
