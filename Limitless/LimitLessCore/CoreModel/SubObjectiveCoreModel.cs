using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class SubObjectiveCoreModel
    {
        private readonly SubObjectiveRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public SubObjectiveCoreModel()
        {
            _repository = new SubObjectiveRepository<object>();
        }

        public int Save(SubObjectiveModel model)
        {
            SqlObject.CommandText = StoredProcedures.SubObjectives.SaveSubObjective;
            int IsActive = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.SubObjectivesID,
                model.ObjectivesID,
                model.SubObjectiveName,
                model.SubObjectiveDescription,
                IsActive,
                model.SubObjectiveCode
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.SubObjectives.GetSubObjectiveList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection ,paginationRequest.OrganizationID};
            var result = _repository.GridList();
            return result;
        }

        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.SubObjectives.GetAllActiveSubObjective;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }

        public SelectedData GetSubObjectiveDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.SubObjectives.GetSubObjectiveById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }
        public int DeleteSubObjective(int id)
        {
            SqlObject.CommandText = StoredProcedures.SubObjectives.DeleteSubObjective;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
