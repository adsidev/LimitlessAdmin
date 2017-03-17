using LimitLessCore.CoreModel;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace LimitLess.Area
{
    /// <summary>
    /// 
    /// </summary>
    public enum excelQue
    {
        SubObjectiveCode = 1,
        QuestionContent = 2,
        QuestionTypeId = 3,
        Difficulty = 4,
        QuestionCode = 5,
        IsActive = 6,
        QuestionImage = 5    //image name is expected to be same as the question code       
    };

    public enum excelAns
    {
        AnswerContent = 0,
        Explanation = 1,
        AnswerCode = 2,
        IsActive = 3,
        IsCorrect = 4
    };

    public class SpreadsheetController : ApiController
    {
        private readonly SpreadsheetCoreModel _coreModel;
        ///// <summary>
        ///// 
        ///// </summary>
        public SpreadsheetController()
        {
            _coreModel = new SpreadsheetCoreModel();
        }

        public List<string> SaveSpreadsheet()
        {
            var httpRequest = HttpContext.Current.Request;
            var stats = new List<int>();              //returned stat from backend
            var statDetails = new List<string>();           //processed stat for user
            var state = 0;                            //0: fail, 1: all success, 2: part success
            if (httpRequest.Files.Count < 1)
            {
                statDetails.Add(state.ToString());
                statDetails.Add("No file is selected");
                return statDetails;
            }
            foreach (string file in httpRequest.Files)
            {
                /*extract excel file and then save the file to App_Data folder*/
                var postedFile = httpRequest.Files[file];
                string uploadFilePath = HttpContext.Current.Server.MapPath("~/App_Data/uploaded_spreadsheet/" + DateTime.Now.ToString("HHmmss") + postedFile.FileName);
                postedFile.SaveAs(uploadFilePath);
                string standardFilePath = HttpContext.Current.Server.MapPath("~/Files/standard_files/excel_format.xlsx");
                /* open the standard excel file and user's excel file */
                var upload_range = openExcelFile(uploadFilePath);
                var standard_range = openExcelFile(standardFilePath);

                /* check if the uploaded file is in the correct format, save the spreadsheet. Otherwise, refuse to save the data. */
                if (!checkExcelFormat(standard_range, upload_range))
                {
                    statDetails.Add(state.ToString());
                    statDetails.Add("The upload excel is not in correct format, please download the empty excel from this webpage.");
                    return statDetails;
                }

                /*save the data to spreadsheet model*/
                List<SpreadsheetModel> spreadsheetList = SaveDataToList(upload_range);
                stats = _coreModel.Save(spreadsheetList);
            }
            statDetails = getStatDetails(stats);
            state = statDetails.Count == 1 ? 1 : 2;
            statDetails.Insert(0, state.ToString());
            return statDetails;
        }

        public Excel.Range openExcelFile(string filePath)
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(filePath);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;
            return range;
        }
        public bool checkExcelFormat(Excel.Range standard_range, Excel.Range upload_range)
        {
            var headerLine = 1;
            var numFixedHead = 11;
            for (int headItem = 1; headItem < numFixedHead; headItem++)
            {
                if (((Excel.Range)standard_range.Cells[headerLine, headItem]).Text != ((Excel.Range)upload_range.Cells[headerLine, headItem]).Text)
                {
                    return false;
                }
            }
            return true;
        }
        public List<SpreadsheetModel> SaveDataToList(Excel.Range range)
        {
            /*question type and difficulty id & name hashtable*/
            Hashtable queTypeId = new Hashtable();
            queTypeId.Add("Fill in the Blank", "1");
            queTypeId.Add("Long Strings", "2");
            queTypeId.Add("String with Image", "3");
            queTypeId.Add("Image Only", "4");
            queTypeId.Add("", "0");

            Hashtable queDiff = new Hashtable();
            queDiff.Add("High", 1);
            queDiff.Add("Medium", 2);
            queDiff.Add("Low", 3);
            queDiff.Add("", 0);

            /*statics for excel data. True means save success to the db, False means save failure */
            var stats = new List<int>();

            var list = new List<SpreadsheetModel>();
            for (int row = 2; row <= range.Rows.Count; row++)
            {
                /* if question content is not null, add the question */
                if (((Excel.Range)range.Cells[row, 6]).Text != "")
                {
                    var queModel = new QuestionModel();
                    var ansList = new List<AnswerModel>();
                    var spr = new SpreadsheetModel
                    {
                        questionModel = queModel,
                        answerList = ansList
                    };

                    spr.questionModel.SubObjectiveCode = ((Excel.Range)range.Cells[row, excelQue.SubObjectiveCode]).Text;
                    spr.questionModel.QuestionContent = ((Excel.Range)range.Cells[row, excelQue.QuestionContent]).Text;
                    spr.questionModel.QuestionCode = ((Excel.Range)range.Cells[row, excelQue.QuestionCode]).Text;
                    spr.questionModel.IsActive = ((Excel.Range)range.Cells[row, excelQue.IsActive]).Text;

                    var typeText = ((Excel.Range)range.Cells[row, excelQue.QuestionTypeId]).Text;
                    spr.questionModel.QuestionTypeId = queTypeId[typeText] != null ? queTypeId[typeText] : "0";

                    var diffText = ((Excel.Range)range.Cells[row, excelQue.Difficulty]).Text;
                    spr.questionModel.Difficulty = queDiff[diffText] != null ? queDiff[diffText] : 0;

                    /*if question type id is 3 or 4, add the questionimage field the question code, otherwise, the field should be null */
                    if (spr.questionModel.QuestionTypeId == "3" || spr.questionModel.QuestionTypeId == "4")
                        spr.questionModel.QuestionImage = ((Excel.Range)range.Cells[row, excelQue.QuestionImage]).Text;
                    else
                        spr.questionModel.QuestionImage = null;

                    /*if answer content is not null, add the answer. */
                    int ans_index = (int)excelQue.IsActive + 1;
                    while (((Excel.Range)range.Cells[row, ans_index]).Text != "")
                    {
                        var ans = new AnswerModel();
                        ans.AnswerContent = ((Excel.Range)range.Cells[row, ans_index + excelAns.AnswerContent]).Text;
                        ans.Explanation = ((Excel.Range)range.Cells[row, ans_index + excelAns.Explanation]).Text;
                        ans.AnswerCode = ((Excel.Range)range.Cells[row, ans_index + excelAns.AnswerCode]).Text;
                        ans.IsActive = ((Excel.Range)range.Cells[row, ans_index + excelAns.IsActive]).Text;
                        ans.IsCorrect = ((Excel.Range)range.Cells[row, ans_index + excelAns.IsCorrect]).Text;
                        spr.answerList.Add(ans);
                        ans_index += 5;
                    }
                    list.Add(spr);
                }

            }
            return list;
        }

        public string getStat(List<int> stats)
        {
            var total = stats.Count();
            var wrong = 0;
            foreach (int i in stats)
            {
                wrong += i == 0 ? 1 : 0;
            }
            return "Total inserted: " + total + "| Successful inserted: " + (total - wrong);
        }
        public List<string> getStatDetails(List<int> stats)
        {
            var statDetails = new List<string>();
            var wrong_cnt = 0;
            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i] == 0)
                {
                    wrong_cnt += 1;
                    statDetails.Add("| Row " + (i + 2).ToString() + " inserted fail.");
                }
            }
            var summary = "Total inserted: " + stats.Count.ToString() + "| Success inserted: " + (stats.Count - wrong_cnt).ToString();
            statDetails.Insert(0, summary);
            return statDetails;
        }
    }
}