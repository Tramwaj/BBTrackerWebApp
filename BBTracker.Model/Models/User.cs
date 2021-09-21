using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Model.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Game> OwnedGames { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }


        public User(Guid id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
            CreatedOn = DateTime.Now;
        }
    }
}
