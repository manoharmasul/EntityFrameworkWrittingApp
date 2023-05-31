using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblPosts")]
    public class PostModel:BaseModel
    {
        //Id,PostContaint,ImageId
        public long Id { get; set; } 
        public string PostContent { get; set; } 
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
        public string PostContent { get; set; }
        public long ImagesId { get; set; }
        public List<ImageModel> Imagelist { get; set; }

    }
}
