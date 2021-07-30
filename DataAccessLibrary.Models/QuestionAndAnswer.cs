using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{

    [Index(nameof(Value))]
    public class QuestionAndAnswer
    {
        public Guid Id { get; set; }
        [Required]
        virtual public Show ShowData { get; set; }
        [Required]
        virtual public Round Round { get; set; }
        [Required]
        virtual public Category Category { get; set; }
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
