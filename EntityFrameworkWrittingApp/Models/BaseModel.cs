namespace EntityFrameworkWrittingApp.Models
{
    public class BaseModel
    {
       
        public long CreatedBy { get; set; }
     
        public long? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public class DeleteObj
        {
            public long Id { get; set; }
            public long ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
