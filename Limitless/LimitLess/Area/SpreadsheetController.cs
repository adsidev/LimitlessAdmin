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
        
        public List<SpreadsheetModel> SaveDataToList(Excel.Range range)
        {
            List<SpreadsheetModel> spreadsheetList = new List<SpreadsheetModel>();
            for (int row = 2; row <= range.Rows.Count; row++)
            {
                if (((Excel.Range) range.Cells[row, 6]).Text != "")
                {
                    var queModel = new QuestionModel();
                    var c_ansModel = new AnswerModel();
                    AnswerModel w1_ansModel = new AnswerModel();
                    AnswerModel w2_ansModel = new AnswerModel();
                    AnswerModel w3_ansModel = new AnswerModel();
                    SpreadsheetModel spreadsheetmodel = new SpreadsheetModel
                    {
                        questionModel = queModel,
                        correctAnswerModel = c_ansModel,
                        wrong1AnswerModel = w1_ansModel,
                        wrong2AnswerModel = w2_ansModel,
                        wrong3AnswerModel = w3_ansModel
                    };
                    spreadsheetmodel.questionModel.SubObjectiveID = Int32.Parse(((Excel.Range)range.Cells[row, 5]).Text, CultureInfo.InvariantCulture);
                    spreadsheetmodel.questionModel.QuestionContent = ((Excel.Range)range.Cells[row, 6]).Text;
                    spreadsheetmodel.questionModel.QuestionTypeId = ((Excel.Range)range.Cells[row, 7]).Text;
                    spreadsheetmodel.questionModel.Difficulty = Int32.Parse(((Excel.Range)range.Cells[row, 8]).Text, CultureInfo.InvariantCulture);
                    spreadsheetmodel.questionModel.QuestionCode = "";
                    spreadsheetmodel.questionModel.IsActive = "true";

                    spreadsheetmodel.correctAnswerModel.AnswerContent = ((Excel.Range)range.Cells[row, 9]).Text;
                    spreadsheetmodel.correctAnswerModel.Explanation = ((Excel.Range)range.Cells[row, 10]).Text;
                    spreadsheetmodel.correctAnswerModel.AnswerCode = "";
                    spreadsheetmodel.correctAnswerModel.IsCorrect = "true";
                    spreadsheetmodel.correctAnswerModel.IsActive = "true";

                    spreadsheetmodel.wrong1AnswerModel.AnswerContent = ((Excel.Range)range.Cells[row, 11]).Text;
                    spreadsheetmodel.wrong1AnswerModel.Explanation = ((Excel.Range)range.Cells[row, 12]).Text;
                    spreadsheetmodel.wrong1AnswerModel.AnswerCode = "";
                    spreadsheetmodel.wrong1AnswerModel.IsCorrect = "false";
                    spreadsheetmodel.wrong1AnswerModel.IsActive = "true";

                    spreadsheetmodel.wrong2AnswerModel.AnswerContent = ((Excel.Range)range.Cells[row, 13]).Text;
                    spreadsheetmodel.wrong2AnswerModel.Explanation = ((Excel.Range)range.Cells[row, 14]).Text;
                    spreadsheetmodel.wrong2AnswerModel.AnswerCode = "";
                    spreadsheetmodel.wrong2AnswerModel.IsCorrect = "false";
                    spreadsheetmodel.wrong2AnswerModel.IsActive = "true";

                    spreadsheetmodel.wrong3AnswerModel.AnswerContent = ((Excel.Range)range.Cells[row, 15]).Text;
                    spreadsheetmodel.wrong3AnswerModel.Explanation = ((Excel.Range)range.Cells[row, 16]).Text;
                    spreadsheetmodel.wrong3AnswerModel.AnswerCode = "";
                    spreadsheetmodel.wrong3AnswerModel.IsCorrect = "false";
                    spreadsheetmodel.wrong3AnswerModel.IsActive = "true";

                    spreadsheetList.Add(spreadsheetmodel);
                }
                
            }
            return spreadsheetList;
        }


    }
}
