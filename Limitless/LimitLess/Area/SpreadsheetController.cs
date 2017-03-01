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
        OrganizationName = 1,
        SubjectName = 2,
        TopicName = 3,
        ObjectiveName = 4,
        SubObjectiveName = 5,
        QuestionContent = 6,
        QuestionTypeId = 7,
        Difficulty = 8,
        QuestionCode = 9,
        IsActive = 10,
        QuestionImage = 9    //image name is expected to be same as the question code       
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

        public bool SaveSpreadsheet()
        { 
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return false;
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
                    return false;
                }

                /*save the data to spreadsheet model*/
                List<SpreadsheetModel> spreadsheetList = SaveDataToList(upload_range);
                _coreModel.Save(spreadsheetList);
                
            }
            return true;
        }

        public Excel.Range openExcelFile(string filePath) {
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
            for (int headItem = 1; headItem < numFixedHead; headItem++) {
                if (((Excel.Range)standard_range.Cells[headerLine, headItem]).Text != ((Excel.Range)upload_range.Cells[headerLine, headItem]).Text) {
                    return false;
                }
            }
            return true;
        }
        public List<SpreadsheetModel> SaveDataToList(Excel.Range range)
        {
            Hashtable queTypeId = new Hashtable();
            queTypeId.Add("Fill in the Blank", "1") ;
            queTypeId.Add("Long Strings", "2");
            queTypeId.Add("String with Image", "3");
            queTypeId.Add("Image Only", "4");

            Hashtable queDiff = new Hashtable();
            queDiff.Add("High", 1);
            queDiff.Add("Medium", 2);
            queDiff.Add("Low", 3);

            var spreadsheetList = new List<SpreadsheetModel>();
            for (int row = 2; row <= range.Rows.Count; row++)
            {
                //if question content is not none, add this row of data
                if (((Excel.Range)range.Cells[row, 6]).Text != "")
                {
                    var queModel = new QuestionModel();
                    var ansList = new List<AnswerModel>();
                    var spr = new SpreadsheetModel
                    {
                        questionModel = queModel,
                        answerList = ansList
                    };
                    //add question

                    /*get subobjective id by subobjective name*/
                    spr.questionModel.SubObjectiveName = ((Excel.Range)range.Cells[row, excelQue.SubObjectiveName]).Text;
                    spr.questionModel.QuestionContent = ((Excel.Range)range.Cells[row, excelQue.QuestionContent]).Text;
                    spr.questionModel.QuestionTypeId = queTypeId[((Excel.Range)range.Cells[row, excelQue.QuestionTypeId]).Text];
                    spr.questionModel.Difficulty = queDiff[((Excel.Range)range.Cells[row, excelQue.Difficulty]).Text];
                    spr.questionModel.QuestionCode = ((Excel.Range)range.Cells[row, excelQue.QuestionCode]).Text;
                    spr.questionModel.IsActive = ((Excel.Range)range.Cells[row, excelQue.IsActive]).Text;

                    /*if question type id is 3 or 4, add the questionimage field the question code, otherwise, the field should be null */
                    if (spr.questionModel.QuestionTypeId == "3" || spr.questionModel.QuestionTypeId == "4")
                        spr.questionModel.QuestionImage = ((Excel.Range)range.Cells[row, excelQue.QuestionImage]).Text;
                    else
                        spr.questionModel.QuestionImage = null; 

                    //if there is answer content, add the answer
                    int ans_index = 12;
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

                    spreadsheetList.Add(spr);
                }

            }
            return spreadsheetList;
        }
        
    }
}
