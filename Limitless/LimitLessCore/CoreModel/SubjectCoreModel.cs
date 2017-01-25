using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class SubjectCoreModel
    {
        private readonly SubjectRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public SubjectCoreModel()
        {
            _repository = new SubjectRepository<object>();
        }

        public int Save(SubjectModel model)
        {
            SqlObject.CommandText = StoredProcedures.Subjects.SaveSubject;
            int staus = 0;
            staus = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.SubjectID,
                model.SubjectName,
                model.SubjectDescription,
                model.SubjectICON,
                model.OrganizationID,
                staus
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Subjects.GetSubjectList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection, paginationRequest.OrganizationID };
            var result = _repository.GridList();
            return result;
        }
        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.Subjects.GetAllSubject;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }
        public SelectedData GetSubjectDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Subjects.GetSubjectById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }
        public int DeleteSubject(int id)
        {
            SqlObject.CommandText = StoredProcedures.Subjects.DeleteSubject;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
