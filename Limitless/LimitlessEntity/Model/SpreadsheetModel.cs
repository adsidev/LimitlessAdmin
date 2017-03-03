
using System.Collections.Generic;

namespace LimitlessEntity.Entities.Models
{

    public class SpreadsheetModel
    {
        public string OrganizationName { get; set; }
        public string SubjectName { get; set; }
        public string TopicName { get; set; }
        public string ObjectiveName { get; set; }
        public QuestionModel questionModel { get; set; }
        public List<AnswerModel> answerList { get; set; }

    }
}
