using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class OrganizationCoreModel
    {
        private readonly OrganizationRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public OrganizationCoreModel()
        {
            _repository = new OrganizationRepository<object>();
        }

        public int Save(OrganizationModel model)
        {
            SqlObject.CommandText = StoredProcedures.Organizations.SaveOrganizations;
            int IsActive = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.OrganizationID,
                model.OrganizationName,
                model.OrganizationAddress,
                model.OrganizationPhone,
                model.OrganizationURL,
                model.OrganizationDescription,
                IsActive
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Organizations.GetOrganizationList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy,paginationRequest.SortDirection, paginationRequest.OrganizationID };
            var result = _repository.GridList();
            return result;
        }

        public SelectedData GetOrgDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Organizations.GetOrganizationById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();
            
            return result;
        }

        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.Organizations.GetAllOrganization;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }
        #endregion
    }
}
