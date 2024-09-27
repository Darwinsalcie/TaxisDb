
namespace TaxisDb.Domain.Models
{
    public class DataResults<TData>
    {
        public DataResults()
        {
            this.Success = true;
        }
        public bool Success { get; set; }
        public string Message {get; set;}
        public TData Result { get; set;}

    }
}
