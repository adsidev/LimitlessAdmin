using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class ObjectiveCoreModel
    {
        private readonly ObjectiveRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public ObjectiveCoreModel()
        {
            _repository = new ObjectiveRepository<object>();
        }

        public int Save(ObjectiveModel model)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.SaveObjective;
            int IsActive = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.ObjectiveID,
                model.TopicID,
                model.ObjectiveName,
                model.ObjectiveDescription,
                IsActive
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.GetObjectiveList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection, paginationRequest.OrganizationID };
            var result = _repository.GridList();
            return result;
        }
        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.GetAllObjectives;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }
        public SelectedData GetObjectiveDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.GetObjectiveById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }
        public int DeleteObjective(int id)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.DeleteObjective;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }

        public SelectedData GetObjectiveIdByName(string name)
        {
            SqlObject.CommandText = StoredProcedures.Objectives.GetObjectiveIdByName;
            SqlObject.Parameters = new object[] { name };
            return _repository.GetOrgDetails();
        }
        #endregion
    }
}
