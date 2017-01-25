using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class OrganizationRepository<T> : RootRepository<T>, IOrganizationRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public OrganizationRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
