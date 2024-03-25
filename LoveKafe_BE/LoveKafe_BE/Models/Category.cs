using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveKafe_BE.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required] 
        public string Code { get; set; }
        [NotMapped]
        public virtual List<Product>? Products { get; set; }
        
        public string? Description { get; set; }
        public string? UrlImage { get; set; }

    }
}
