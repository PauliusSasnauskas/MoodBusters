using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodBustersWebAPI.Database
{
    public class LogRecord
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public ulong ByteCount { get; set; }
    }
}