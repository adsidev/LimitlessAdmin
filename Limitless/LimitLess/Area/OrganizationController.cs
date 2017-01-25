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
    public class OrganizationController : ApiController
    {
        private readonly OrganizationCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public OrganizationController()
        {
            _coreModel = new OrganizationCoreModel();
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
        public bool SaveOrganization(OrganizationModel objOrganization)
        {
            bool result = false;
            int noOfrecrd=_coreModel.Save(objOrganization);
            if (noOfrecrd>0)
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

        [HttpPost]
        public SelectedData GetOrgDetails(RequestData Obj)
        {
            return _coreModel.GetOrgDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
    }
}
