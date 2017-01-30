namespace LimitlessEntity.Entities.Models
{
    public class AnswerModel
    {
        public string AnswerID { get; set; }
        public string QuestionID { get; set; }
        public string AnswerCode { get; set; }
        public string AnswerContent { get; set; }
        public string IsCorrect { get; set; }
        public string Explanation { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
    }
    public class UserAnswerModel
    {
        public string AnswerID { get; set; }
        public string QuestionID { get; set; }
        public string UserID { get; set; }
    }
    public class AnswerStatus
    {
        public string AnswerResult { get; set; }
    }
}
