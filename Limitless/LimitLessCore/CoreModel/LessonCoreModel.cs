using LimitLessDataAccess;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using LimitLessRepository;
using LimitLessRepository.Repositories.Datastore;

namespace LimitLessCore.CoreModel
{
    public class LessonCoreModel
    {
        private readonly LessonRepository<object> _repository;

        #region "Constructors"
        ///<summary>
        /// Constructor for initialize 
        ///</summary>
        public LessonCoreModel()
        {
            _repository = new LessonRepository<object>();
        }

        public int Save(LessonModel model)
        {
            SqlObject.CommandText = StoredProcedures.Lessons.SaveLesson;
            int IsActive = model.IsActive.Equals("true") ? 1 : 0;
            SqlObject.Parameters = new object[]
            {
                model.LessonID,
                model.LessonContent,
                IsActive
            };
            return _repository.Save();
        }
        public GridResult GridList(GridRequest paginationRequest)
        {
            SqlObject.CommandText = StoredProcedures.Lessons.GetLessonList;
            SqlObject.Parameters = new object[] { paginationRequest.PageIndex, paginationRequest.PageSize, paginationRequest.OrderBy, paginationRequest.SortDirection};
            var result = _repository.GridList();
            return result;
        }
        public SelectedData GetLessonDetails(int id)
        {
            SqlObject.CommandText = StoredProcedures.Lessons.GetLessonById;
            SqlObject.Parameters = new object[] { id };
            var result = _repository.GetOrgDetails();

            return result;
        }
        public int DeleteLesson(int id)
        {
            SqlObject.CommandText = StoredProcedures.Lessons.DeleteLesson;
            SqlObject.Parameters = new object[] { id };
            int result = _repository.Delete(id);
            return result;
        }
        #endregion
    }
}
