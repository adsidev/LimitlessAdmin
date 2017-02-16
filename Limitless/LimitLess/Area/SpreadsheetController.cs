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


namespace LimitLess.Area
{
    /// <summary>
    /// 
    /// </summary>
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

        public HttpResponseMessage SaveSpreadsheet()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                /*extract excel file and then save the file to App_Data folder*/
                var postedFile = httpRequest.Files[file];
                string filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + DateTime.Now.ToString("HHmmss") + postedFile.FileName);
                postedFile.SaveAs(filePath);

                /*read data from the excel file*/
                ReadDataFromFile(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);

        }

        public void ReadDataFromFile(string filePath)
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(filePath);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;

            /*save the data to spreadsheet model*/
            List<SpreadsheetModel> spreadsheetList = SaveDataToList(range);
            _coreModel.Save(spreadsheetList);
        }

        public string extractID(string str)
        {

            var regex = new Regex(@"([1-9]+)");
            var results = regex.Matches(str);
            var id = results[0].Groups[1].Value;
            return id;
        }

        public List<SpreadsheetModel> SaveDataToList(Excel.Range range)
        {
            List<SpreadsheetModel> spreadsheetList = new List<SpreadsheetModel>();
            for (int row = 2; row <= range.Rows.Count; row++)
            {
                //if question content is not none, add this row of data
                if (((Excel.Range) range.Cells[row, 6]).Text != "")
                {
                    var queModel = new QuestionModel();
                    var ansList = new List<AnswerModel>();
                    var spr = new SpreadsheetModel
                    {
                        questionModel = queModel,
                        answerList = ansList
                    };
                    //add question
                    spr.questionModel.SubObjectiveID = Int32.Parse(extractID(((Excel.Range)range.Cells[row, 5]).Text), CultureInfo.InvariantCulture);
                    spr.questionModel.QuestionContent = ((Excel.Range)range.Cells[row, 6]).Text;
                    spr.questionModel.QuestionTypeId = extractID(((Excel.Range)range.Cells[row, 5]).Text);

                    spr.questionModel.Difficulty = Int32.Parse(extractID(((Excel.Range)range.Cells[row, 8]).Text), CultureInfo.InvariantCulture);
                    spr.questionModel.QuestionCode = ((Excel.Range)range.Cells[row, 9]).Text;
                    spr.questionModel.IsActive = ((Excel.Range)range.Cells[row, 10]).Text;
                    //if there is answer content, add the answer
                    int ans_index = 11;
                    while (((Excel.Range)range.Cells[row, ans_index]).Text != "")
                    {
                        var ans = new AnswerModel();
                        ans.AnswerContent = ((Excel.Range)range.Cells[row, ans_index]).Text;
                        ans.Explanation = ((Excel.Range)range.Cells[row, ans_index+1]).Text;
                        ans.AnswerCode = ((Excel.Range)range.Cells[row, ans_index+2]).Text;
                        ans.IsActive = ((Excel.Range)range.Cells[row, ans_index+3]).Text;
                        ans.IsCorrect = ((Excel.Range)range.Cells[row, ans_index+4]).Text; 

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
