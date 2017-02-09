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
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                Trace.WriteLine(filePath);
                // NOTE: To store in memory use postedFile.InputStream
                //Read data from excel file
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(filePath);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                Excel.Range range = worksheet.UsedRange;
                for (int row = 2; row <= range.Rows.Count; row++)
                {
                    
                }
            }

            return Request.CreateResponse(HttpStatusCode.Created);

        }
    }
}
