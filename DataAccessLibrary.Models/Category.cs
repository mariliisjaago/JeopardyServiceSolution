using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string CategoryName { get; set; }
    }
}
