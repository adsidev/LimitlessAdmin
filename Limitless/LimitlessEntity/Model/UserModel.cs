namespace LimitlessEntity.Entities.Models
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string OrganizationID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string DeviceType { get; set; }
        public string DeviceToken { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string UserType { get; set; }
    }
    public class UserLives
    {
        public string UserLivesID { get; set; }
        public string UserID { get; set; }
        public string LivesID { get; set; }
        public string IsDeleted { get; set; }

    }
}
