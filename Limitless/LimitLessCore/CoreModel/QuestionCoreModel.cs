using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class QuestionCoreModel
    {
        private readonly QuestionRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public QuestionCoreModel()
        {
            _repository = new QuestionRepository<object>();
        }

        public int Save(QuestionModel model)
        {

            SqlObject.CommandText = StoredProcedures.Questions.SaveQuestion;
            int IsActive = model.IsActive.ToLower().Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.QuestionID,
                model.SubObjectiveID,
                model.QuestionCode,
                model.QuestionContent,
                model.Difficulty,
                IsActive,
                model.QuestionTypeId,
                model.QuestionImage
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetQuestionList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection, paginationRequest .OrganizationID};
            var result = _repository.GridList();
            return result;
        }
        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetAllQuestions;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }
        public ListResult GetQuestionType()
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetQuestionType;
            return _repository.GetData();
        }
        public SelectedData GetQuestionDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetQuestionById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();
            return result;
        }

        public ListResult GetLastQuestionId()
        {
            SqlObject.CommandText = StoredProcedures.Questions.GetLastQuestionId;
            //SqlObject.Parameters = new object[] { };
            return _repository.GetData();
        }

        public int DeleteQuestion(int id)
        {
            SqlObject.CommandText = StoredProcedures.Questions.DeleteQuestion;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
