using LimitlessEntity.Results.Security;
using LimitLessRepository.Repositories.Interfaces;
using LimitLessRepository.Common;
using LimitLessDataAccess;
using Newtonsoft.Json;
namespace LimitLessRepository.Repositories.Datastore
{
    public class LoginRepository<T> : RootRepository<T>, ILoginRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;

        public LoginRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
        public UserData GetUserData(LoginRequest model)
        {
            var response = new UserData();
            var ds = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Login.LoginValidation, model.Email, model.Password);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                }
            }
            if (response.StatusMsg == "SUCCESS")
                response.IsAuthenticated = true;
            return response;
        }
        public UserDetails GetUserInformation(LoginRequest model)
        {
            var response = new UserDetails();
            var ds = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Login.LoginValidation, model.Email, model.Password);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    response.UserInformations = JsonConvert.SerializeObject(ds.Tables[0]);
                }
            }

            return response;
        }
    }
}
