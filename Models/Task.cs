using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruebaTecnicaImagineAppTareas.Models
{
    [Table("tasks")]
    public class Task
    {
        [Column("id")]
        [Required]
        public int Id { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("title")]
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column("status")]
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Column("due_date")]
        public DateTime? DueDate { get; set; }
    }
}
