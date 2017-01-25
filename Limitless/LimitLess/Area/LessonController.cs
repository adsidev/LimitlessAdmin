using LimitLessCore.CoreModel;
using LimitlessEntity.Entities.Models;
using LimitlessEntity.Request;
using LimitlessEntity.Results;
using System.Web.Http;

namespace LimitLess.Area
{
    public class LessonController : ApiController
    {
        private readonly LessonCoreModel _coreModel;

        /// <summary>
        /// 
        /// </summary>
        public LessonController()
        {
            _coreModel = new LessonCoreModel();
        }

        // GET: api/Profiles
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost]
        public GridResult GridList([FromBody]GridRequest paginationRequest)
        {
            return _coreModel.GridList(paginationRequest);
            //return new string[] { "value1", "value2" };
        }
        [Authorize]
        [HttpPost]
        public int SaveLesson(LessonModel LesModel)
        {
            return _coreModel.Save(LesModel);
        }

        [HttpPost]
        public SelectedData GetLessonDetails(RequestData Obj)
        {
            return _coreModel.GetLessonDetails(Obj.ID);
        }
        [Authorize]
        [HttpPost]
        public int DeleteLesson(RequestData Obj)
        {
            return _coreModel.DeleteLesson(Obj.ID);
        }
    }
}
