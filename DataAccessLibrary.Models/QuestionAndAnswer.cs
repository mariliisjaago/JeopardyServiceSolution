using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{

    [Index(nameof(Value))]
    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        [Required]
        public Show ShowData { get; set; }
        [Required]
        public Round Round { get; set; }
        [Required]
        [MaxLength(150)]
        public Category Category { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        [MaxLength(900)]
        public string Question { get; set; }
        [Required]
        [MaxLength(150)]
        public string Answer { get; set; }
    }
}
