using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class SpreadsheetCoreModel
    {
        private readonly SpreadsheetRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public SpreadsheetCoreModel()
        {
            _repository = new SpreadsheetRepository<object>();
        }

        public int Save(SpreadsheetModel model)
        {
            SqlObject.CommandText = StoredProcedures.Organizations.SaveOrganizations;
            SqlObject.Parameters = new object[]
            {
                model.QuestionID,
                model.QuestionContent,
                model.QuestionType,
                model.QuestionDifficulty,
                model.QuestionExplanation,
                model.CorrectAnswer,
                model.WrongAnswer1,
                model.WrongAnswer2,
                model.WrongAnswer3
            };
            return _repository.Save();
        }
    }
    #endregion
}
