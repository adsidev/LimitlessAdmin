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
    public class UserController : ApiController
    {
        private readonly UserCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public UserController()
        {
            _coreModel = new UserCoreModel();
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

        [Authorize]
        [HttpPost]
        public UserData SaveUser(UserModel SModel)
        {
            return _coreModel.Save(SModel);
            //return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public SelectedData GetUserDetails(RequestData Obj)
        {
            return _coreModel.GetUserDetails(Obj.ID);
            //return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public SelectedData GetUserLives(RequestData Obj)
        {
            return _coreModel.GetUserLives(Obj.ID);
            //return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public int SaveUserLives(UserLives SModel)
        {
            return _coreModel.SaveUserLives(SModel);
            //return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public int DeleteUser(RequestData Obj)
        {
            return _coreModel.DeleteUser(Obj.ID);
        }
        [HttpPost]
        public SelectedData GetUserScore(RequestData Obj)
        {
            return _coreModel.GetUserScore(Obj.ID);
        }
    }
}
