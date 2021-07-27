using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Round
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string RoundName { get; set; }
    }
}
