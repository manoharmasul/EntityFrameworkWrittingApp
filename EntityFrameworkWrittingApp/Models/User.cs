using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EntityFrameworkWrittingApp.Models
{
    
    public class UserRegistrationModel : BaseModel
    {
        public long? Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? Password { get; set; }
        public long? RoleId { get; set; }
        public List<User> userlists { get; set; }
    }
    public class GetUserModel : BaseModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

    }
    public class UserLogIn : BaseModel
    {
        //   Id,UserName,EmailId,MobileNo,Passoword,RoleId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        //Id,FisrtName,LastName,UserName,EmailAddress,MobileNo,Password,DateOfBirth
        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Password { get; set; }
    }
    public class GetUserProfileModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string Bio { get; set; }
        public string Links { get; set; }
        public List<GetAllPostsModel> GetAllPosts { get; set; }
        public long NoOfFollower { get; set; }
        public long NoOfFollowing { get; set; }
        public long NoOfPosts { get; set; }
        public byte[] ImageData { get; set; }

    }
  
    public class UserModelIds
    {
        public long UserId { get; set; }      
    }

}
