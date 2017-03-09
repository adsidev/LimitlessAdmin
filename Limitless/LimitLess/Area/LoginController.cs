using LimitLessCore;
using LimitlessEntity.Results.Security;
using System.Web.Http;
using System.Web.Security;

namespace LimitLess.Area
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// Core Model
        /// </summary>
        private readonly LoginCoreModel _model;
        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public LoginController()
        {
            _model = new LoginCoreModel();
        }
        #endregion
        // GET api/documentation
        /// <summary>
        /// This is how we create a documentation
        // GET: api/Login/Validate
        /// </summary> 
        [HttpPost]
        public UserDetails GetUserData(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserType))
                request.UserType = "1";
            return _model.GetUserData(request);
        }

        public UserDetails GetUserLoginData(LoginRequest request)
        {
            return _model.GetUserLoginData(request);
        }
    }
}
