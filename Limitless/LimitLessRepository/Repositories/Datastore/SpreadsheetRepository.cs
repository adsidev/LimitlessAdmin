using LimitLessRepository;
using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class SpreadsheetRepository<T> : RootRepository<T>, ISpreadsheetRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public SpreadsheetRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
