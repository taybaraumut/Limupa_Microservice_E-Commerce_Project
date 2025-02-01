namespace Limupa.Basket.Api.Services.GoogleCloudStorageServices
{
    public interface ICloudStorageService
    {
        Task<string> GetSignedUrlProductImageAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        Task DeleteFileAsync(string fileNameToDelete);
        Task<string?> GenerateFileNameToSave(string fileName);
    }
}
