namespace LimitlessEntity.Entities.Models
{
    public class QuestionModel
    {
        public string QuestionID { get; set; }
        public string SubObjectiveName { get; set; }
        public int SubObjectiveID { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionContent { get; set; }
        public int Difficulty { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string QuestionType { get; set; }
        public string QuestionTypeId { get; set; }
        public string QuestionImage { get; set; }
    }
}
