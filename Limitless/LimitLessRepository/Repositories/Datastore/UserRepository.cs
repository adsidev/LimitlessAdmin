using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class UserRepository<T> : RootRepository<T>, IUserRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public UserRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
