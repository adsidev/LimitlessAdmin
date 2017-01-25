using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitlessEntity.Results
{
    public class GridResult
    {
        //public  List<dynamic>  List{ get; set; }
        public string List { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ListResult
    {
        //public  List<dynamic>  List{ get; set; }
        public string List { get; set; }
    }
    public class SelectedData
    {
        public string SelectedDetails { get; set; }
    }
    public class SelectedQA
    {
        public string SelectedQuestion { get; set; }
        public string RelatedAnswers { get; set; }
    }
    public class ListInput
    {
        public int OrganizationID { get; set; }
    }
}
