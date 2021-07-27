using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Round
    {
        public int Id { get; set; }
        [Required]
        public string RoundName { get; set; }
    }
}
