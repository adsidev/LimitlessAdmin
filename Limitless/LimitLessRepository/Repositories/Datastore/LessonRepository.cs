using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class LessonRepository<T> : RootRepository<T>, ILessonRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public LessonRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
