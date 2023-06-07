using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblImagesBackground")]
    public class ImagesModel:BaseModel
    {
        public long Id { get; set; }
        public string Filename { get; set; }
        public byte[] ImageData { get; set; }
    }
}
