namespace LoveKafe_BE.Models
{
    public class Register
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string? UrlImage { get; set; }
    }
}
