using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class TopicRepository<T> : RootRepository<T>, ITopicRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public TopicRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
