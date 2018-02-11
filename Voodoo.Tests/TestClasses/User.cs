using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;

namespace Voodoo.Tests.TestClasses
{
    public class User
    {
        public User()
        {
            
            Roles = new List<Role>();
            Supervisors = new List<User>();
            DirectReports = new List<User>();
            IsActive = true;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Role> Roles { get; set; }
        public List<User> Supervisors { get; set; }
        public List<User> DirectReports { get; set; }      
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public Level Level { get; set; }
        public bool IsActive { get; set; }     
        public bool? IsLocked { get; set; }
    }
}
