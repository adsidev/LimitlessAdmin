using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;
using System.Collections.Generic;

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

        //public int Save(List<SpreadsheetModel> spreadsheetList)
        //{
        //    int ret = 1;
        //    for (int row = 0; row < spreadsheetList.Count; row++) {
        //        SqlObject.CommandText = StoredProcedures.Spreadsheet.SaveSpreadsheet;
        //        SqlObject.Parameters = new object[]
        //        {
        //            spreadsheetList[row].SubObjectiveID,
        //            spreadsheetList[row].QuestionContent,
        //            spreadsheetList[row].QuestionDifficulty,
        //            spreadsheetList[row].QuestionTypeID,

        //            spreadsheetList[row].CorrectAnswer_Content,
        //            spreadsheetList[row].CorrectAnswer_Explanation,

        //            spreadsheetList[row].WrongAnswer1_Content,
        //            spreadsheetList[row].WrongAnswer1_Explanation,

        //            spreadsheetList[row].WrongAnswer2_Content,
        //            spreadsheetList[row].WrongAnswer2_Explanation,
        //            spreadsheetList[row].WrongAnswer3_Content,
        //            spreadsheetList[row].WrongAnswer3_Explanation


        //        };
        //        ret = _repository.Save();
        //        if (ret == 0) {
        //            return ret;
        //        }
        //    }
        //    return ret;
        //}

        public int Save(SpreadsheetModel spreadsheet)
        {

            SqlObject.CommandText = StoredProcedures.Spreadsheet.SaveSpreadsheet;
            SqlObject.Parameters = new object[]
            {
                    spreadsheet.SubObjectiveID,
                    spreadsheet.QuestionContent,
                    spreadsheet.QuestionDifficulty,
                    spreadsheet.QuestionTypeID,
                    null,
                    null,
                    null,

                    spreadsheet.CorrectAnswer_Content,
                    spreadsheet.CorrectAnswer_Explanation,
                    null,
                    null,
                    null,
                    null,
                    null,

                    spreadsheet.WrongAnswer1_Content,
                    spreadsheet.WrongAnswer1_Explanation,
                    null,
                    null,
                    null,
                    null,
                    null,

                    spreadsheet.WrongAnswer2_Content,
                    spreadsheet.WrongAnswer2_Explanation,
                    null,
                    null,
                    null,
                    null,
                    null,

                    spreadsheet.WrongAnswer3_Content,
                    spreadsheet.WrongAnswer3_Explanation,
                    null,
                    null,
                    null,
                    null,
                    null
            };

            return _repository.Save();
        }
    }
    #endregion
}
