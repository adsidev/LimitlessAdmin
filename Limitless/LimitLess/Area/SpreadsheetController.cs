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
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                Trace.WriteLine(filePath);
                // NOTE: To store in memory use postedFile.InputStream
                //Read data from excel file
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(filePath);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                Excel.Range range = worksheet.UsedRange;
                List<SpreadsheetModel> spreadsheetList = new List<SpreadsheetModel>();
                for (int row = 2; row <= range.Rows.Count; row++)
                {
                    SpreadsheetModel model = new SpreadsheetModel();

                    model.SubObjectiveID = ((Excel.Range)range.Cells[row, 5]).Text;
                    model.QuestionContent = ((Excel.Range)range.Cells[row, 6]).Text;
                    model.QuestionDifficulty = ((Excel.Range)range.Cells[row, 8]).Text;
                    model.QuestionTypeID = ((Excel.Range)range.Cells[row, 7]).Text;

                    model.CorrectAnswer_Content = ((Excel.Range)range.Cells[row, 9]).Text;
                    model.CorrectAnswer_Explanation = ((Excel.Range)range.Cells[row, 10]).Text;

                    model.WrongAnswer1_Content = ((Excel.Range)range.Cells[row, 11]).Text;
                    model.WrongAnswer1_Explanation = ((Excel.Range)range.Cells[row, 12]).Text;

                    model.WrongAnswer2_Content = ((Excel.Range)range.Cells[row, 13]).Text;
                    model.WrongAnswer2_Explanation = ((Excel.Range)range.Cells[row, 14]).Text;

                    model.WrongAnswer3_Content = ((Excel.Range)range.Cells[row, 15]).Text;
                    model.WrongAnswer3_Explanation = ((Excel.Range)range.Cells[row, 16]).Text;

                    spreadsheetList.Add(model);
                    _coreModel.Save(model);
                }
               // _coreModel.Save(spreadsheetList);
                
            }

            return Request.CreateResponse(HttpStatusCode.Created);

        }
    }
}
