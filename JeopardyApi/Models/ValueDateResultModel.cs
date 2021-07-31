using DataAccessLibrary.Models;

namespace JeopardyApi.Models
{
    public class ValueDateResultModel
    {
        public int DifferenceInDays { get; set; }
        public QuestionAndAnswer QuestionData { get; set; }
    }
}
