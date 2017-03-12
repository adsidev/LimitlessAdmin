using LimitLessCore.CoreModel;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using System.Web.Http;
using LimitlessEntity.Entities.Models;
using System.Web;
using System;
using Newtonsoft.Json;

namespace LimitLess.Area
{
    public class QuestionController : ApiController
    {
        private readonly QuestionCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public QuestionController()
        {
            _coreModel = new QuestionCoreModel();
        }

        // GET: api/Profiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost]
        public GridResult GridList([FromBody]GridRequest paginationRequest)
        {
            return _coreModel.GridList(paginationRequest);
        }

        

        [Authorize]
        [HttpPost]
        public bool SaveQuestion(QuestionModel question)
        {
            bool result = false;
            int noOfrecrd = _coreModel.Save(question);
            if (noOfrecrd > 0)
            {
                result = true;
            }
            return result;
        }

        [Authorize]
        [HttpPost]
        public bool SaveQuestionWithImage()
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
                var fileName = DateTime.Now.ToString("HHmmss") + postedFile.FileName;
                string uploadFilePath = HttpContext.Current.Server.MapPath("~/images/question_Image/" + fileName);
                postedFile.SaveAs(uploadFilePath);
                var jsonString = httpRequest.Params["parameters"];
                var questionData = JsonConvert.DeserializeObject<QuestionModel>(jsonString);
                questionData.QuestionImage = fileName;
                var result = SaveQuestion(questionData);

            }

            return true;
        }

        [Authorize]
        [HttpPost]
        public ListResult GetList(ListInput Input)
        {
            return _coreModel.GetList(Input.OrganizationID);
        }
        [Authorize]
        [HttpGet]
        public ListResult GetQuestionType()
        {
            return _coreModel.GetQuestionType();
        }
        [HttpPost]
        public SelectedData GetQuestionDetails(RequestData Obj)
        {
            return _coreModel.GetQuestionDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int DeleteQuestion(RequestData Obj)
        {
            return _coreModel.DeleteQuestion(Obj.ID);
        }
    }
}
