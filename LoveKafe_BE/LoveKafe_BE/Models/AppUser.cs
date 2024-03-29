using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LoveKafe_BE.Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        public Guid UserDetailId { get; set; }
        [JsonIgnore]
        public virtual UserDetail UserDetail { get; set; }

    }
}
