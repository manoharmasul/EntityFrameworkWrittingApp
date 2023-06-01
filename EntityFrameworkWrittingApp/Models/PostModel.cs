using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblPosts")]
    public class PostModel:BaseModel
    {
        //Id,PostContaint,ImageId
        public long Id { get; set; } 
        public string PostContaint { get; set; } 
        public long ImagesId { get; set; } 
        
    }
    [Table("tblImages")]
    public class ImageModel:BaseModel
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
    }
    public class GetImagePostModel : BaseModel
    {
        //Id,PostContaint,ImageId
        public long Id { get; set; }
        public string PostContaint { get; set; }
        public long ImagesId { get; set; }
        public List<ImageModel> Imagelist { get; set; }

    }
    public class GetAllPostsModel
    {
        //CreateBy as UserId,UserName,Id as PostId,PostContaint,ImageUser
        //Id,PostContaint,ImageId
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long PostId { get; set; }
        public string PostContaint { get; set; }
        public string ImageUrl { get; set; }

    }
    [Table("tblLikes")]
    public class LikeModel:BaseModel
    {//Id,PostId,UserId,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,IsDeleted

        public long Id { get; set; }    
        public long PostId { get; set; }    
        public long UserId { get; set; }    
    }
}
