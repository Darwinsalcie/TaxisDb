using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxisDb.Persistence.Models.UserGroup
{
    public class UserGroupModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }
        public string? Description { get; set; }
    }
}
