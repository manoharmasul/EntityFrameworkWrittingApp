using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblUser")]
    public class User : BaseModel
    {
        //Id,FisrtName,LastName,UserName,EmailAddress,MobileNo,Password,DateOfBirth
        public long Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public long MobileNo { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
