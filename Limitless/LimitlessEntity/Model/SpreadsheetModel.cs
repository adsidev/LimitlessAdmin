
namespace LimitlessEntity.Entities.Models
{
    public class SpreadsheetModel
    {
        /*question in excel*/
        public string SubObjectiveID { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionDifficulty { get; set; }
        public string QuestionTypeID { get; set; }
        /*question in excel*/

        /*question assign default value*/
        public int QuestionID { get; set; }
        public string QuestionCode { get; set; }
        public bool IsActive { get; set; }
        /*question assign default value*/

        /*correct answer in excel*/
        public string CorrectAnswer_Content { get; set; }
        public string CorrectAnswer_Explanation { get; set; }
        /*correct answer in excel*/

        /*correct answer assign default value*/
        public string CorrectAnswer_AnswerID { get; set; }
        public string CorrectAnswer_QuestionID { get; set; }
        public string CorrectAnswer_AnswerCode { get; set; }
        public string CorrectAnswer_IsCorrect { get; set; }
        public string CorrectAnswer_IsActive { get; set; }
        /*correct answer assign default value*/

        /*wrong answer 1 in excel*/
        public string WrongAnswer1_Content { get; set; }
        public string WrongAnswer1_Explanation { get; set; }
        /*wrong answer 1 in excel*/

        /*wrong answer 1 assign default value*/
        public string WrongAnswer1_AnswerID { get; set; }
        public string WrongAnswer1_QuestionID { get; set; }
        public string WrongAnswer1_AnswerCode { get; set; }
        public string WrongAnswer1_IsCorrect { get; set; }
        public string WrongAnswer1_IsActive { get; set; }
        /*wrong answer 1 assign default value*/

        /*wrong answer 2 in excel*/
        public string WrongAnswer2_Content { get; set; }
        public string WrongAnswer2_Explanation { get; set; }
        /*wrong answer 2 in excel*/

        /*wrong answer 2 assign default value*/
        public string WrongAnswer2_AnswerID { get; set; }
        public string WrongAnswer2_QuestionID { get; set; }
        public string WrongAnswer2_AnswerCode { get; set; }
        public string WrongAnswer2_IsCorrect { get; set; }
        public string WrongAnswer2_IsActive { get; set; }
        /*wrong answer 2 assign default value*/

        /*wrong answer 3 in excel*/
        public string WrongAnswer3_Content { get; set; }
        public string WrongAnswer3_Explanation { get; set; }
        /*wrong answer 3 in excel*/

        /*wrong answer 3 assign default value*/
        public string WrongAnswer3_AnswerID { get; set; }
        public string WrongAnswer3_QuestionID { get; set; }
        public string WrongAnswer3_AnswerCode { get; set; }
        public string WrongAnswer3_IsCorrect { get; set; }
        public string WrongAnswer3_IsActive { get; set; }
        /*wrong answer 3 assign default value*/
    }
}
