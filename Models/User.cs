using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruebaTecnicaImagineAppTareas.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        [Required]
        [MaxLength(100)]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

    }
}
