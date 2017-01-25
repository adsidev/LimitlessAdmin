using LimitLessCore.CoreModel;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using System.Web.Http;
using LimitlessEntity.Entities.Models;

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
        public ListResult GetList(ListInput Inpurt)
        {
            return _coreModel.GetList(Inpurt.OrganizationID);
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
