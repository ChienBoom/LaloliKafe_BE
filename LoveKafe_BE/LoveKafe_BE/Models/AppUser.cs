using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoveKafe_BE.Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        public Guid UserDetailId { get; set; }
        public virtual UserDetail UserDetail { get; set; }

    }
}
