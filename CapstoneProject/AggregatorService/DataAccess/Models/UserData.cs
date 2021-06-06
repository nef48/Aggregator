using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.DataAccess.Models
{
    public class UserData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastLogin { get; set; }
        public List<Topic> Topics { get; set; }

        public UserData()
        {
            Topics = new List<Topic>();
        }

        public UserData(UserData user)
        {
            UserID = user.UserID;
            Username = user.Username;
            Password = user.Password;
            DateCreated = user.DateCreated;
            LastLogin = user.LastLogin;
            Topics = user.Topics;
        }
    }
}
