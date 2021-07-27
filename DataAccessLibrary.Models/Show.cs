using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Show
    {
        public int Id { get; set; }
        [Required]
        public int ShowNumber { get; set; }
        [Required]
        public DateTime AirDate { get; set; }
    }
}
