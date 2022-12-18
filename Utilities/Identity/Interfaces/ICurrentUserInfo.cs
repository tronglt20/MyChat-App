namespace Utilities.Interfaces
{
    public interface ICurrentUserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CurrentUserInfo : ICurrentUserInfo
    {
        public CurrentUserInfo()
        {

        }

        public CurrentUserInfo(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
