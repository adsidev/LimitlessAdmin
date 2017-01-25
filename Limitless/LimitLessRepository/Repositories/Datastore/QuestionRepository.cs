using LimitLessRepository.Common;
using LimitLessRepository.Repositories.Interfaces;

namespace LimitLessRepository.Repositories.Datastore
{
    public class QuestionRepository<T> : RootRepository<T>, IQuestionRepository<T> where T : class
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connectionString;
        public QuestionRepository()
        {
            _connectionString = Constants.LimitLessConnectionString;
        }
    }
}
