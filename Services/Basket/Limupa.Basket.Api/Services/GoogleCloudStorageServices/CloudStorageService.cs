using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Limupa.Basket.Api.CloudStorage.ConfigOptions;
using Microsoft.Extensions.Options;


namespace Limupa.Basket.Api.Services.GoogleCloudStorageServices
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly GCSConfigOptions _options;
        private readonly ILogger<CloudStorageService> _logger;
        private readonly GoogleCredential _googleCredential;

        public CloudStorageService(IOptions<GCSConfigOptions> options, ILogger<CloudStorageService> logger)
        {       
            _options = options.Value;
            _logger = logger;

            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environment == Environments.Production)
                {
                    // Store the json file in Secrets.
                    _googleCredential = GoogleCredential.FromJson(_options.GCPStorageAuthFile);
                }
                else
                {
                    _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }
        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName,fileNameToDelete);
                }
                _logger.LogInformation($"File {fileNameToDelete} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while deleting file {fileNameToDelete}: {ex.Message}");
                throw;
            }
        }

        public async Task<string?> GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }

        public async Task<string> GetSignedUrlProductImageAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            try
            {
                var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
                var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
                var signedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));

                _logger.LogInformation($"Signed url obtained and updated in database for file {fileNameToRead}");
                return signedUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while obtaining or updating signed url for file {fileNameToRead}: {ex.Message}");
                throw;
            }

        }

        public async Task<string> RefreshSignedUrlAsync(string fileNameToRead, int timeOutInMinutes )
        {
            //// Burada yeni bir imzalı URL alabiliriz, aynı mantığı kullanarak
            //// Örneğin:
            //var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
            //var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
            //var refreshedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));

            // Bu örnekte, imzalı URL'yi aynı şekilde alıyoruz, ama siz isteğinize göre bu kısmı değiştirebilirsiniz
            var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
            var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
            var refreshedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));

            return refreshedUrl.ToString();
        }

        public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                _logger.LogInformation($"Uploading: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        // upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                        _logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }
    }
}

