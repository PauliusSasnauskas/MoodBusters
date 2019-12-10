using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MoodBustersWebAPI.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Ip { get; set; }
        [Required]
        public string Name { get; set; }
    }
}