using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveKafe_BE.Models
{
    [Table("orderDetail")]
    public class OrderDetail
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public virtual Order? Order { get; set; }
        [NotMapped]
        public virtual Product? Product { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
