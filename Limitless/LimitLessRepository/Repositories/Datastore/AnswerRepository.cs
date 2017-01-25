using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class AnswerRepository<T> : RootRepository<T>, IAnswerRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public AnswerRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
