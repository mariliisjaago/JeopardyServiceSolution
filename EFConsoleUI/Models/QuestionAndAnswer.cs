using System;

namespace EFConsoleUI.Models
{
    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        public int ShowNumber { get; set; }
        public DateTime AirDate { get; set; }
        public string Round { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public string Currency { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
