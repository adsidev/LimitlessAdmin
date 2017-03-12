using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Datastore;
using System.Collections.Generic;

namespace LimitLessCore.CoreModel
{
    public class AnswerCoreModel
    {
        private readonly AnswerRepository<object> _repository;

        public object JsonConvert { get; private set; }

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public AnswerCoreModel()
        {
            _repository = new AnswerRepository<object>();
        }

        public int Save(AnswerModel model)
        {
            SqlObject.CommandText = StoredProcedures.Answers.SaveAnswer;
            int IsActive = model.IsActive.ToLower().Equals("true") ? 1 : 0;
            int IsCorrect = model.IsCorrect.ToLower().Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.AnswerID,
                model.QuestionID,
                model.AnswerCode,
                model.AnswerContent,
                IsCorrect,
                model.Explanation,
                IsActive

            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Answers.GetAnswerList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection, paginationRequest.OrganizationID };
            var result = _repository.GridList();
            return result;
        }

        public SelectedData GetAnswerDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Answers.GetAnswerById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();
            return result;
        }
        public SelectedQA GetSelectedQA(int id)
        {
            SqlObject.CommandText = StoredProcedures.Answers.GetDetailsById;
            string _connectionString = "";
            _connectionString = Constants.LimitLessConnectionString;
            SqlObject.Parameters = new object[] { id };
            var result = new SelectedQA();
            var ds = SqlHelper.ExecuteDataset(_connectionString, SqlObject.CommandText, SqlObject.Parameters);
            result.SelectedQuestion = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            result.RelatedAnswers = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1]);
            return result;
        }
        public int DeleteAnswer(int id)
        {
            SqlObject.CommandText = StoredProcedures.Answers.DeleteAnswer;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        public AnswerStatus SaveUserAnswer(UserAnswerModel model)
        {
            SqlObject.CommandText = StoredProcedures.Answers.SaveUserAnswer;
            SqlObject.Parameters = new object[]
            {
                model.QuestionID,
                model.AnswerID,
                model.UserID
            };
            return _repository.SaveUserAnswer();
        }

        public GridResult GetQuestionAnswerList(GridQARequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetQuestionAnswerList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection, paginationRequest.OrganizationID, paginationRequest.SubjectID };
            var result = _repository.GridQAList();
            return result;
        }

        public void SaveQuestionAnswer(MainQuestionAnswers model)
        {

                foreach (var row in model.QuestionAnswer)
                {
                    SqlObject.CommandText = StoredProcedures.Answers.SaveQuestionAnswer;
                    SqlObject.Parameters = new object[]
                    {
                        row.QuestionID,
                        row.AnswerID,
                        row.UserID,
                        row.AttemptDate,
                        row.Streak
                    };
                    _repository.SaveQuestionAnswer();
                }
            
            


                SqlObject.CommandText = StoredProcedures.Answers.SaveQuestionAnswerSubjective;
                SqlObject.Parameters = new object[]
                {
                    model.SubObjectiveAnswer.UserID,
                    model.SubObjectiveAnswer.SubObjectiveID,
                    model.SubObjectiveAnswer.RightsCount,
                    model.SubObjectiveAnswer.WrongCount,
                    model.SubObjectiveAnswer.SubObjectiveScoreDate,
                    model.SubObjectiveAnswer.RightsGlobalCount,
                    model.SubObjectiveAnswer.WrongGlobalCount
                };
                _repository.SaveQuestionAnswerSubjective();
            
        }
        #endregion
    }
}
