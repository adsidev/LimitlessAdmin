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
    public class ObjectiveController : ApiController
    {
        private readonly ObjectiveCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public ObjectiveController()
        {
            _coreModel = new ObjectiveCoreModel();
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
        public ListResult GetList(ListInput Inpurt)
        {
            return _coreModel.GetList(Inpurt.OrganizationID);
        }
        [Authorize]
        [HttpPost]
        public int SaveObjective(ObjectiveModel SModel)
        {
            return _coreModel.Save(SModel);
            //return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public SelectedData GetObjectiveDetails(RequestData Obj)
        {
            return _coreModel.GetObjectiveDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int DeleteObjective(RequestData Obj)
        {
            return _coreModel.DeleteObjective(Obj.ID);
        }
    }
}
