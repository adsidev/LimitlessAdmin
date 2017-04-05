using System;
using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class TopicCoreModel
    {
        private readonly TopicRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public TopicCoreModel()
        {
            _repository = new TopicRepository<object>();
        }

        public int Save(TopicModel model)
        {
            SqlObject.CommandText = StoredProcedures.Topics.SaveTopics;
            int Isactive = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.TopicID,
                model.SubjectID,
                model.TopicName,
                model.TopicDescription,
                Isactive,
                model.TopicCode
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Topics.GetTopicList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy,paginationRequest.SortDirection, paginationRequest.OrganizationID };
            var result = _repository.GridList();
            return result;
        }

        public ListResult GetList(int OrganizationID)
        {
            SqlObject.CommandText = StoredProcedures.Topics.GetAllTopics;
            SqlObject.Parameters = new object[] { OrganizationID };
            return _repository.GetList();
        }

        public SelectedData GetTopicDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Topics.GetTopicById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }
        public int DeleteTopic(int id)
        {
            SqlObject.CommandText = StoredProcedures.Topics.DeleteTopic;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
