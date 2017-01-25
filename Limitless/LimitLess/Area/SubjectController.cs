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
    public class SubjectController : ApiController
    {
        private readonly SubjectCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public SubjectController()
        {
            _coreModel = new SubjectCoreModel();
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
        public int SaveSubject(SubjectModel SModel)
        {
            return _coreModel.Save(SModel);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public ListResult GetList(ListInput Inpurt)
        {
            return _coreModel.GetList(Inpurt.OrganizationID);
        }
        [HttpPost]
        public SelectedData GetSubjectDetails(RequestData Obj)
        {
            return _coreModel.GetSubjectDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int DeleteSubject(RequestData Obj)
        {
            return _coreModel.DeleteSubject(Obj.ID);
        }
    }
}
