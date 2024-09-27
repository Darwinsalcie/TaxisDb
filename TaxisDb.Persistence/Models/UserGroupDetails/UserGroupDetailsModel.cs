using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxisDb.Persistence.Models.UserGroupDetails
{
    public class UserGroupDetailsModel
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
