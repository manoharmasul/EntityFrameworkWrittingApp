using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IImagesAsyncRepository
    {
        Task<long> UpLoadImages(ImageModels imgs);
        Task<long> UpLoadBackgroundImages(ImagesBackground imgs);
        Task<List<ImageModels>> GetImages();
        Task<List<ImagesBackground>> GetAllBackgroundImages();

    }
}
