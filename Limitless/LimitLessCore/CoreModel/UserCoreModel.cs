using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class UserCoreModel
    {
        private readonly UserRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public UserCoreModel()
        {
            _repository = new UserRepository<object>();
        }

        public UserData Save(UserModel model)
        {
            SqlObject.CommandText = StoredProcedures.Users.SaveUser;
            int staus = 0;
            staus = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.UserID,
                model. OrganizationID,
                model. UserName,
                model. Password,
                model. FName,
                model. LName,
                model. Email,
                staus,
                model.UserType
            };
            UserData objUser = new UserData();
            objUser = _repository.SaveTrans();
            return objUser;
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Users.GetUser;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection };
            var result = _repository.GridList();
            return result;
        }
        public SelectedData GetUserDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Users.GetUserById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }

        public SelectedData GetUserLives(int id)
        {
            SqlObject.CommandText = StoredProcedures.Users.GetUserLivesById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }

        public SelectedData GetUserScore(int id)
        {
            SqlObject.CommandText = StoredProcedures.Users.GetUserScoreById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }

        public int SaveUserLives(UserLives model)
        {
            SqlObject.CommandText = StoredProcedures.Users.SaveUserLives;
            SqlObject.Parameters = new object[]
            {
                model.UserLivesID,
                model.UserID,
                model.LivesID
            };
            return _repository.Save();
        }
        public int DeleteUser(int id)
        {
            SqlObject.CommandText = StoredProcedures.Users.DeleteUser;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
