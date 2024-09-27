namespace TaxisDb.Web.Models.BaseDTO
{
    public abstract record BaseRemoveEntityDTO
    {
        public BaseRemoveEntityDTO() { 
        
            DeletedDate = DateTime.Now;
        }
        public int DeletedUser { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
