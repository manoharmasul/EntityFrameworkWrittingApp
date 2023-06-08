using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{

    [Table("tblFollowers")]
    public class FollowersAndFollowingModel:BaseModel
    {
        //Id,FollowedId,FollowingId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long FollowedId { get; set; }
        public long FollowingId { get; set; }      
        public bool? IsFollow { get; set; }      
    }
    public class GetUserFollowModel
    {
        public long  UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string? Name { get; set; }
        public bool? IsFollow { get; set; }
        public byte[]? ImageData { get; set; }
      
    }
}
