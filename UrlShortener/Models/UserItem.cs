namespace UrlShortener.Models
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

        public override string ToString()
        {
            return Id + ":" + Username + ":" + Password + ":" + Admin;
        }

        public UserItem(int id, string username, string password, bool admin)
        {
            Id = id;
            Username = username;
            Password = password;
            Admin = admin;
        }
    }
}
