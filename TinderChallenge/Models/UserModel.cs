using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderChallenge.Models
{
    public enum LookingFor
    {
        Women = 1,
        Men = 2,
        Both = 3
    };

    public enum Gender
    {
        Female = 1,
        Male = 2,
        Other = 3
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string ProfilePicture { get; set; }
        public LookingFor InterestedInFinding { get; set; }
        public List<int> Likes { get; set; }
        public List<int> Passes { get; set; }
    }
}
