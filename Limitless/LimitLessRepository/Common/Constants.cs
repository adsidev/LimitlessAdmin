using System.Configuration;

namespace LimitLessRepository.Common
{
    public class Constants
    {
        /// <summary>
        /// iPaas database connection string
        /// </summary>
        public static readonly string LimitLessConnectionString = ConfigurationManager.ConnectionStrings["LimitLess"].ConnectionString;
    }
}
