using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoveKafe_BE.Models
{
    [Table("table")]
    public class Table
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public Guid AreaId { get; set; }
        [NotMapped]
        public virtual Area? Area { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual List<Order>? Orders { get; set; }
        public string? Description { get; set; }
    }
}
