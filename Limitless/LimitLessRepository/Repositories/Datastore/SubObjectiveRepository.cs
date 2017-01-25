using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class SubObjectiveRepository<T> : RootRepository<T>, ISubObjectiveRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public SubObjectiveRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
