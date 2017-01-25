using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class SubjectRepository<T> : RootRepository<T>, ISubjectRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public SubjectRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
