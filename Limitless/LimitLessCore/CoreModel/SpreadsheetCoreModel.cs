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
            foreach(var spreadsheet in  spreadsheetList)
            {
                //save question
                _questionCoreModel.Save(spreadsheet.questionModel);

                //get the question id of the saved question
                var jString = _questionCoreModel.GetLastQuestionId().List;
                var regex = new Regex(@":([1-9]+)");
                var results = regex.Matches(jString);
                var que_id = results[0].Groups[1].Value;

                //save answers of the question
                foreach (var ans in spreadsheet.answerList)
                {
                    ans.QuestionID = que_id;
                    _answerCoreModel.Save(ans);
                }
            }

            return ret;
        }
    }
    #endregion
}
