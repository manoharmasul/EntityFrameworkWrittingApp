using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblUser")]
    public class User : BaseModel
    {
        //Id,UserName,EmailId,MobileNo,Passoword,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        //Id,FisrtName,LastName,UserName,EmailAddress,MobileNo,Password,DateOfBirth
        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Password { get; set; }
    }
}
