

namespace TaxisDb.Domain.Base
{
    public abstract class AuditEntity<TType> : BaseEntity<TType>
    {

        public AuditEntity()
        {

            this.CreationDate = DateTime.Now;
            this.Deleted = false;
        }

        public DateTime CreationDate { get; set; }
        public int CreationUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUser { get; set; }
        public bool? Deleted { get; set; }


        //public AuditEntity(DateTime CreationDate, DateTime? CreationUser, int? ModifyUser, DateTime? DeletedDate, int? DeletedUser, bool? Deleted)
        //{


        //    this.CreationDate = CreationDate;
        //    this.CreationUser = CreationUser;
        //    this.ModifyDate = ModifyDate;
        //    this.ModifyUser = ModifyUser;
        //    this.DeletedDate = DeletedDate;
        //    this.DeletedUser = DeletedUser;
        //    this.Deleted = Deleted;

        //}


    }
}
