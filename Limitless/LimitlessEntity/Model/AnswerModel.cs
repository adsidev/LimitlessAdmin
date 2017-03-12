using System.Collections.Generic;
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

    public class QuestionAnswer
    {
        public System.Data.DataSet QuestionDataSet { get; set; } 
    }

    public class AnswerStatus
    {
        public string AnswerResult { get; set; }
    }


    public class SubObjectiveAnswers
    {
        public string RightsGlobalCount { get; set; }
        public string SubObjectiveID { get; set; }
        public int RightsCount { get; set; }
        public int WrongCount { get; set; }
        public int UserID { get; set; }
        public string SubObjectiveScoreDate { get; set; }
        public string WrongGlobalCount { get; set; }
    }

    public class QuestionAnswers
    {
        public string AnswerID { get; set; }
        public string AttemptDate { get; set; }
        public string QuestionID { get; set; }
        public string Streak { get; set; }
        public int UserID { get; set; }
    }

    public class MainQuestionAnswers
    {
        public SubObjectiveAnswers SubObjectiveAnswer { get; set; }
        public List<QuestionAnswers> QuestionAnswer { get; set; }
    }

}
