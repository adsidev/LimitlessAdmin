using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web;
using Newtonsoft.Json.Linq;


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

        readonly QuestionCoreModel _questionCoreModel = new QuestionCoreModel();
        readonly AnswerCoreModel _answerCoreModel = new AnswerCoreModel();
        
        public int Save(List<SpreadsheetModel> spreadsheetList)
        {
            int ret = 1;
            foreach(SpreadsheetModel item in  spreadsheetList)
            {
                _questionCoreModel.Save(item.questionModel);
                //get the question id using regular expression
                var jString = _questionCoreModel.GetLastQuestionId().List;
                var regex = new Regex(@":([1-9]+)");
                var results = regex.Matches(jString);
                var que_id = results[0].Groups[1].Value;
                item.correctAnswerModel.QuestionID = que_id;
                _answerCoreModel.Save(item.correctAnswerModel);
                item.wrong1AnswerModel.QuestionID = que_id;
                _answerCoreModel.Save(item.wrong1AnswerModel);
                item.wrong2AnswerModel.QuestionID = que_id;
                _answerCoreModel.Save(item.wrong2AnswerModel);
                item.wrong3AnswerModel.QuestionID = que_id;
                _answerCoreModel.Save(item.wrong3AnswerModel);
            }

            return ret;
        }
    }
    #endregion
}
