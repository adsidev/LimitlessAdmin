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
    public class TopicController : ApiController
    {
        private readonly TopicCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public TopicController()
        {
            _coreModel = new TopicCoreModel();
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
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int SaveTopics(TopicModel topicModel)
        {
            return _coreModel.Save(topicModel);
        }
        [Authorize]
        [HttpPost]
        public ListResult GetList(ListInput Inpurt)
        {
            return _coreModel.GetList(Inpurt.OrganizationID);
        }

        [HttpPost]
        public SelectedData GetTopicDetails(RequestData Obj)
        {
            return _coreModel.GetTopicDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int DeleteTopic(RequestData Obj)
        {
            return _coreModel.DeleteTopic(Obj.ID);
        }
    }
}
