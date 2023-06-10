using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    public class MainModels
    {
    }
    [Table("tblUser")]
    public class User : BaseModel
    {

        //Id,UserName,EmailId,MobileNo,Passoword,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        //Id,FisrtName,LastName,UserName,EmailAddress,MobileNo,Password,DateOfBirth

        public long Id { get; set; }
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string EmailId { get; set; }
        public string? UserProfile { get; set; }
        public string? Bio { get; set; }
        public string? Links { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

    }
    [Table("tblUserProfile")]
    public class UserProfileImages : BaseModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Filename { get; set; }
        public byte[]? ImageData { get; set; }
    }
    [Table("tblImagesBackground")]
    public class ImagesBackground : BaseModel
    {
        public long Id { get; set; }
        public string Filename { get; set; }
        public byte[]? ImageData { get; set; }
    }

}
