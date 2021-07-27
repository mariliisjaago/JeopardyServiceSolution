using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string CategoryName { get; set; }
    }
}
