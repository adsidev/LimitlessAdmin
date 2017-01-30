using LimitLessCore.CoreModel;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using System.Web.Http;

namespace LimitLess.Area
{
    /// <summary>
    /// 
    /// </summary>
    public class AnswerController : ApiController
    {
        private readonly AnswerCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public AnswerController()
        {
            _coreModel = new AnswerCoreModel();
        }

        // GET: api/Profiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public GridResult GridList([FromBody]GridRequest paginationRequest)
        {
            return _coreModel.GridList(paginationRequest);
            //return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public SelectedData GetAnswerDetails(RequestData Obj)
        {
            return _coreModel.GetAnswerDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpPost]
        public bool SaveAnswer(AnswerModel answer)
        {
            bool result = false;
            int noOfrecrd = _coreModel.Save(answer);
            if (noOfrecrd > 0)
            {
                result = true;
            }
            return result;
        }
        [HttpPost]
        public SelectedQA GetSelectedQA(RequestData Obj)
        {
            return _coreModel.GetSelectedQA(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public int DeleteAnswer(RequestData Obj)
        {
            return _coreModel.DeleteAnswer(Obj.ID);
        }
        [HttpPost]
        public AnswerStatus SaveUserAnswer(UserAnswerModel UserAnswer)
        {
            return _coreModel.SaveUserAnswer(UserAnswer);
        }
    }
}
