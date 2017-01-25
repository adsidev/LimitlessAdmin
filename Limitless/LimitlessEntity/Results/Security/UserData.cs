using LimitLessUtility.Common;
using System.Runtime.Serialization;

namespace LimitlessEntity.Results.Security
{
    public class UserData : BasicResult
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string AdminFlag { get; set; }
        [DataMember]
        public string PwdRecCount { get; set; }
        [DataMember]
        public string StatusMsg { get; set; }
        [DataMember]
        public string PwdExpDays { get; set; }
        [DataMember]
        public bool IsAuthenticated { get; set; }
        [DataMember]
        public string DeviceTocken { get; set; }
    }
    public class UserDetails
    {
        public string UserInformations { get; set; }
    }
}
