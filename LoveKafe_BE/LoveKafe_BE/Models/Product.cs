using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoveKafe_BE.Models
{
    [Table("product")]
    public class Product
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
        public Guid CategoryId { get; set; }
        [NotMapped]
        public Category? Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual List<OrderDetail>? OrderDetails { get; set; }
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
    }
}
