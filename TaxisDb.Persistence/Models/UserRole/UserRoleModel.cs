using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxisDb.Domain.Base;
using TaxisDb.Domain.Entities;

namespace TaxisDb.Persistence.Models.UserRole
{
    public class UserRoleModel
    {
        //public UserRoleKey Id { get ; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
