namespace Utilities.Interfaces
{
    public interface ICurrentUserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class CurrentUserInfo : ICurrentUserInfo
    {
        public CurrentUserInfo()
        {

        }

        public CurrentUserInfo(int id, string userName, string email)
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
