using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class ObjectiveRepository<T> : RootRepository<T>, IObjectiveRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public ObjectiveRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
