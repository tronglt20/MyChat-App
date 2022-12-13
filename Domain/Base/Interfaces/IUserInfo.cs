namespace Domain.Base.Interfaces
{
    public interface IUserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public UserInfo()
        {

        }

        public UserInfo(int id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
