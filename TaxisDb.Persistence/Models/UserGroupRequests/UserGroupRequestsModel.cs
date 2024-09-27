using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxisDb.Persistence.Models.UserGroupRequests
{
    public class UserGroupRequestsModel
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
