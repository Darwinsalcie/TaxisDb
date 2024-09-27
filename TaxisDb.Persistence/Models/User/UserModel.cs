using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxisDb.Persistence.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public int UserGroupReqId { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
