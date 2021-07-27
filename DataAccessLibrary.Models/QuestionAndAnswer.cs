using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        [Required]
        public int ShowNumber { get; set; }
        [Required]
        public string Round { get; set; }
        [Required]
        [MaxLength(150)]
        public string Category { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        [MaxLength(900)]
        public string Question { get; set; }
        [Required]
        [MaxLength(150)]
        public string Answer { get; set; }
    }
}
