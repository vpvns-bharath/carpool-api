using System.ComponentModel.DataAnnotations;

namespace CarpoolApi.Models
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }
        public String Email { get; set; }

        public String Password { get; set; }
    }
}
