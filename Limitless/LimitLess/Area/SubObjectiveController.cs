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
    public class SubObjectiveController : ApiController
    {
        private readonly SubObjectiveCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public SubObjectiveController()
        {
            _coreModel = new SubObjectiveCoreModel();
        }

        // GET: api/Profiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [System.Web.Http.HttpPost]
        public GridResult GridList([FromBody]GridRequest paginationRequest)
        {
            return _coreModel.GridList(paginationRequest);
            //return new string[] { "value1", "value2" };
        }

        // POST: api/Profiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// 
        [Authorize]
        [System.Web.Http.HttpPost]
        public int SaveSubObjective(SubObjectiveModel model)
        {
            return _coreModel.Save(model);
        }
        [Authorize]
        [HttpPost]
        public ListResult GetList(ListInput Inpurt)
        {
            return _coreModel.GetList(Inpurt.OrganizationID);
        }


        [HttpPost]
        public SelectedData GetSubObjectiveDetails(RequestData Obj)
        {
            return _coreModel.GetSubObjectiveDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int DeleteSubObjective(RequestData Obj)
        {
            return _coreModel.DeleteSubObjective(Obj.ID);
        }
    }
}
