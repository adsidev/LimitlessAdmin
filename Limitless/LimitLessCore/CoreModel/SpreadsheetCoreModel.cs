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
using System.Collections;


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
        readonly SubObjectiveCoreModel _subobjCoreModel = new SubObjectiveCoreModel();

        public List<int> Save(List<SpreadsheetModel> spreadsheetList)
        {
            List<int> stats = new List<int>();
            foreach (var spr in spreadsheetList)
            {
                var ret = checkQueAndAns(spr);
                stats.Add(ret);
                if (ret != 0)
                {
                    saveQueAndAns(spr);
                }
            }
            return stats;
        }

        public int checkQueAndAns(SpreadsheetModel spr)
        {
            /*check question*/
            var que_ret = checkQue(spr.questionModel);
            if (que_ret == 0){  return 0; }              
           
            /*check answers*/
            foreach (var ans in spr.answerList)
            {
                 if (checkAns(ans) == 0) { return 0; }    
            }
            return 1;
        }

        public int checkQue(QuestionModel que)
        {
            var subObjId = _subobjCoreModel.GetSubObjectiveIdByName(que.SubObjectiveName).SelectedDetails;
            if (subObjId == "[]" || que.Difficulty == 0 || que.QuestionTypeId == "0" || que.QuestionCode == "")
                return 0;

            return 1;
        }

        public int checkAns(AnswerModel ans)
        {
            if (ans.AnswerCode == "")
                return 0;
            return 1;
        }

        public int saveQueAndAns(SpreadsheetModel spr)
        {
            /*save question*/
            var que = spr.questionModel;
            var subObjId = _subobjCoreModel.GetSubObjectiveIdByName(que.SubObjectiveName).SelectedDetails;
            que.SubObjectiveID = Int32.Parse(extractID(subObjId));
            _questionCoreModel.Save(que);

            /*save answers*/
            var que_id = extractID(_questionCoreModel.GetLastQuestionId().List);
            foreach (var ans in spr.answerList)
            {
                ans.QuestionID = que_id;
                _answerCoreModel.Save(ans);
            }
            return 1;
        }

        public string extractID(string str)
        {
            var regex = new Regex(@":([0-9]+)");
            var results = regex.Matches(str);
            /*check errors*/
            var id = results[0].Groups[1].Value;
            return id;
        }
        #endregion
    }
}
