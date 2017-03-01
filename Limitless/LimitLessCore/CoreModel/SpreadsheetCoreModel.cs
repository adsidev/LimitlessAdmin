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

        readonly QuestionCoreModel     _questionCoreModel = new QuestionCoreModel();
        readonly AnswerCoreModel       _answerCoreModel   = new AnswerCoreModel();
        readonly SubObjectiveCoreModel _subobjCoreModel   = new SubObjectiveCoreModel();
        
        public int Save(List<SpreadsheetModel> spreadsheetList)
        {
            int ret = 1;
            foreach(var spreadsheet in  spreadsheetList)
            {
                /*get subobjective id by name, then save question*/
                var subObjId = _subobjCoreModel.GetSubObjectiveIdByName(spreadsheet.questionModel.SubObjectiveName).SelectedDetails;
                spreadsheet.questionModel.SubObjectiveID = Int32.Parse(extractID(subObjId));
                _questionCoreModel.Save(spreadsheet.questionModel);

                /*get question id, then save answers*/
                var que_id = extractID(_questionCoreModel.GetLastQuestionId().List);
                foreach (var ans in spreadsheet.answerList)
                {
                    ans.QuestionID = que_id;
                    _answerCoreModel.Save(ans);
                }
            }
            return ret;
        }
        public string extractID(string str)
        {
            var regex = new Regex(@":([0-9]+)");
            var results = regex.Matches(str);
            /*check errors*/
            var id = results[0].Groups[1].Value;
            return id;
        }
    }
    #endregion
}
