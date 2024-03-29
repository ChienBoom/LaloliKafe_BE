using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoveKafe_BE.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public Guid TableId { get; set; }
        [NotMapped]
        public virtual Table? Table { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual List<OrderDetail>? OrderDetails { get; set; }

    }
}
