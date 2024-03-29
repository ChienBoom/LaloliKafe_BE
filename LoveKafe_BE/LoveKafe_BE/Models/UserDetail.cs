using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveKafe_BE.Models
{
    [Table("userDetail")]
    public class UserDetail
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        [NotMapped]
        public virtual AppUser? AppUser { get; set; }
        public string? UrlImage { get; set; }
    }
}
