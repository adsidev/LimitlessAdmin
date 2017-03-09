using System;
using System.Text;
using System.Threading;
using System.Security.Principal;
using System.Web;
using LimitlessEntity.Results.Security;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore
{
    public class LoginCoreModel
    {
        /// <summary>
        /// Login view Model
        /// </summary>
        private readonly LoginRepository<object> _repository;
        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public LoginCoreModel()
        {
            _repository = new LoginRepository<object>();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserDetails GetUserData(LoginRequest request)
        {
            return _repository.GetUserInformation(request);
        }

        public UserDetails GetUserLoginData(LoginRequest request)
        {
            return _repository.GetUserLoginInformation(request);
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private static bool AuthenticateUser(string credentials)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials));

            var credentialsArray = credentials.Split(':');
            var username = credentialsArray[0];
            var password = credentialsArray[1];

            /* REPLACE THIS WITH REAL AUTHENTICATION
            ----------------------------------------------*/
            if (!(username == "test" && password == "test"))
            {
                return false;
            }

            var identity = new GenericIdentity(username);
            SetPrincipal(new GenericPrincipal(identity, null));

            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            throw new NotImplementedException();
        }
    }
}
