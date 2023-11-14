using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCrudApi.Db.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }


        // Navigation property for the many-to-many relationship
        public ICollection<UserRoleMap> UserRoleMaps { get; set; }
    }
}
