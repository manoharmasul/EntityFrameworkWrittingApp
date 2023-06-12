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
  
    public class GetImagePostModel : BaseModel
    {
        //Id,PostContaint,ImageId
        public long Id { get; set; }
        public string PostContaint { get; set; }
        public long ImagesId { get; set; }
        public List<ImagesBackground> Imagelist { get; set; }

    }
    public class GetAllPostsModel
    {
        //CreateBy as UserId,UserName,Id as PostId,PostContaint,ImageUser
        //Id,PostContaint,ImageId
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public long PostId { get; set; }
        public string PostContaint { get; set; }
        public string ImageUrl { get; set; }
        public List<LikeModel> likemodel { get; set; }
        public List<GetCommentsModel> commentsmodel { get; set; }
        public byte[]? ImageData { get; set; }
        public byte[]? ImageDatabg { get; set; }
        public long NoOfLikes { get; set; }
        public long NoOfComments { get; set; }

    }
    [Table("tblLikes")]
    public class LikeModel:BaseModel
    {//Id,PostId,UserId,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,IsDeleted
        //PostId,UserId,CreatedBy,IsDeleted,LikeOrDislike
        public long Id { get; set; }    
        public long PostId { get; set; }    
        public long UserId { get; set; }    
        public bool LikeOrDislike { get; set; }    
    }

    [Table("tblComments")]
    public class CommentsModel : BaseModel
    {
        ////Id,PostId,UserId,Comments,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        //Id,PostId,UserId,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,IsDeleted
        //PostId,UserId,CreatedBy,IsDeleted,LikeOrDislike
        public long Id { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Comments { get; set; }
    }
    public class GetCommentsModel 
    {
        ////Id,PostId,UserId,Comments,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        //Id,PostId,UserId,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,IsDeleted
        //PostId,UserId,CreatedBy,IsDeleted,LikeOrDislike
        public long Id { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public string UserProfile { get; set; }
        public byte[]? ImageData { get; set; }

    }

}
