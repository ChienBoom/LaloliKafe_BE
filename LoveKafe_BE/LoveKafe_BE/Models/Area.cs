using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoveKafe_BE.Models
{
    [Table("area")]
    public class Area
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required] 
        public string Code { get; set; }
        public bool IsDelete { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual List<Table>? Tables { get; set; }
        public string? Description { get; set; }
    }
}
