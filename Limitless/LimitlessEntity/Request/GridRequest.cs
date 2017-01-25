namespace LimitlessEntity.Request
{
    public class GridRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
        public string OrganizationID { get; set; }
    }
    public class RequestData
    {
        public int ID { get; set; }
    }
}
