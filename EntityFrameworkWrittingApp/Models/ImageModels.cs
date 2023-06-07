using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("Images")]
    public class ImageModels
    {      
            public long Id { get; set; }
            public string Filename { get; set; }
            public byte[] ImageData { get; set; }
        
    }
    public class ImageModelsDetails
    {
         
        public ImageModels Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
