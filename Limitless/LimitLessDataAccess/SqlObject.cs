using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitLessDataAccess
{
    public class SqlObject
    {
        public static string CommandText { get; set; }
        public static object[] Parameters { get; set; }
    }
}
