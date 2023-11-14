using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCrudApi.Db.Models
{
    public class User : IMustHaveTenant
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        // Navigation property for the many-to-many relationship
        public ICollection<UserRoleMap> UserRoleMaps { get; set; }


    }
}
