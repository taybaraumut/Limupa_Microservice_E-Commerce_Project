using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Limupa.Basket.Api.Services.GoogleCloudStorageServices;
using Limupa.Catalog.Api.CloudStorage.ConfigOptions;
using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Dtos.ProductImageDtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.GoogleCloudStorageServices
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

        public async Task GoogleStorageProductImageAdditionControlAsync(CreateProductImageDto createProductImageDto)
        {
            if (createProductImageDto.BigPhoto != null && createProductImageDto.BigPhoto.Count > 0)
            {
                createProductImageDto.ProductBigImageUrl = new List<string>();
                createProductImageDto.BigSavedFileName = new List<string>();
                createProductImageDto.BigSavedUrl = new List<string>();

                for (int i = 0; i < createProductImageDto.BigPhoto.Count; i++)
                {
                    var photo = createProductImageDto.BigPhoto[i];
                    var savedFileName = GenerateFileNameToSave(photo.FileName);
                    var savedUrl = await UploadFileAsync(photo, savedFileName!);
                    var productBigImageUrl = await GetSignedUrlProductImageAsync(savedFileName!);

                    createProductImageDto.ProductBigImageUrl.Add(productBigImageUrl);
                    createProductImageDto.BigSavedFileName.Add(savedFileName!);
                    createProductImageDto.BigSavedUrl.Add(savedUrl);
                }
            }

            if (createProductImageDto.SmallPhoto != null && createProductImageDto.SmallPhoto.Count > 0)
            {
                createProductImageDto.ProductSmallImageUrl = new List<string>();
                createProductImageDto.SmallSavedFileName = new List<string>();
                createProductImageDto.SmallSavedUrl = new List<string>();

                for (int i = 0; i < createProductImageDto.SmallPhoto.Count; i++)
                {
                    var photo = createProductImageDto.SmallPhoto[i];
                    var savedFileName = GenerateFileNameToSave(photo.FileName);
                    var savedUrl = await UploadFileAsync(photo, savedFileName!);
                    var productSmallImageUrl = await GetSignedUrlProductImageAsync(savedFileName!);

                    // Listeye ekleme
                    createProductImageDto.ProductSmallImageUrl.Add(productSmallImageUrl);
                    createProductImageDto.SmallSavedFileName.Add(savedFileName!);
                    createProductImageDto.SmallSavedUrl.Add(savedUrl);
                }
            }
        }

        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToDelete);
                }
                _logger.LogInformation($"File {fileNameToDelete} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while deleting file {fileNameToDelete}: {ex.Message}");
                throw;
            }
        }

        public string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }

        public async Task GenerateSignedUrlOne(GetByIdProductDto getByIdProductDto)
        {
            
            if (!string.IsNullOrWhiteSpace(getByIdProductDto.SavedFileName))
            {
                getByIdProductDto.ProductImageUrl = await GetSignedUrlProductImageAsync(getByIdProductDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductByElectronicDeviceCategoryAsync(ResultProductByElectronicDeviceCategoryDto resultProductByElectronicDeviceCategoryDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductByElectronicDeviceCategoryDto.SavedFileName))
            {
                resultProductByElectronicDeviceCategoryDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductByElectronicDeviceCategoryDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductImageAsync(GetProductImageByProductIdDto getProductImageByProductIdDto)
        {
            if (getProductImageByProductIdDto.BigSavedFileName != null && getProductImageByProductIdDto.BigSavedFileName.Any())
            {
                // Initialize the ProductImageUrl list with the same size as SavedFileName list
                getProductImageByProductIdDto.ProductBigImageUrl = new List<string>(new string[getProductImageByProductIdDto.BigSavedFileName.Count]);

                // Iterate through each SavedFileName and get the signed URL
                for (int i = 0; i < getProductImageByProductIdDto.BigSavedFileName.Count; i++)
                {
                    var savedFileName = getProductImageByProductIdDto.BigSavedFileName[i];
                    if (!string.IsNullOrWhiteSpace(savedFileName))
                    {
                        var signedUrl = await GetSignedUrlProductImageAsync(savedFileName);
                        getProductImageByProductIdDto.ProductBigImageUrl[i] = signedUrl;
                    }
                    else
                    {
                        getProductImageByProductIdDto.ProductBigImageUrl[i] = null!; // Assign null for empty or whitespace file names
                    }
                }
            }
            if (getProductImageByProductIdDto.SmallSavedFileName != null && getProductImageByProductIdDto.SmallSavedFileName.Any())
            {
                // Initialize the ProductImageUrl list with the same size as SavedFileName list
                getProductImageByProductIdDto.ProductSmallImageUrl = new List<string>(new string[getProductImageByProductIdDto.SmallSavedFileName.Count]);

                // Iterate through each SavedFileName and get the signed URL
                for (int i = 0; i < getProductImageByProductIdDto.SmallSavedFileName.Count; i++)
                {
                    var savedFileName = getProductImageByProductIdDto.SmallSavedFileName[i];
                    if (!string.IsNullOrWhiteSpace(savedFileName))
                    {
                        var signedUrl = await GetSignedUrlProductImageAsync(savedFileName);
                        getProductImageByProductIdDto.ProductSmallImageUrl[i] = signedUrl;
                    }
                    else
                    {
                        getProductImageByProductIdDto.ProductSmallImageUrl[i] = null!; // Assign null for empty or whitespace file names
                    }
                }
            }
        }

        public async Task GenerateSignedUrlProductLastTenAsync(ResultProductLastTenDto resultProductLastTenDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductLastTenDto.SavedFileName))
            {
                resultProductLastTenDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductLastTenDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductMackbookLaptopModelAsync(ResultProductMackbookLaptopModelDto resultProductMackbookLaptopModelDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductMackbookLaptopModelDto.SavedFileName))
            {
                resultProductMackbookLaptopModelDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductMackbookLaptopModelDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductPersonelCareCategoryAsync(ResultTenDataByProductPersonelCareCategoryDto resultTenDataByProductPersonelCareCategoryDto)
        {
            if (!string.IsNullOrWhiteSpace(resultTenDataByProductPersonelCareCategoryDto.SavedFileName))
            {
                resultTenDataByProductPersonelCareCategoryDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultTenDataByProductPersonelCareCategoryDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductVideoGameModelAsync(ResultProductVideoGameModelDto resultProductVideoGameModelDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductVideoGameModelDto.SavedFileName))
            {
                resultProductVideoGameModelDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductVideoGameModelDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductİpohnePhoneModelAsync(ResultProductİpohnePhoneModelDto resultProductİpohnePhoneModelDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductİpohnePhoneModelDto.SavedFileName))
            {
                resultProductİpohnePhoneModelDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductİpohnePhoneModelDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlTwo(ResultProductWithCategoryDto resultProductWithCategoryDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductWithCategoryDto.SavedFileName))
            {
                resultProductWithCategoryDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductWithCategoryDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductBluetoothListAsync(ResultProductBluetoothDto resultProductBluetoothDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductBluetoothDto.SavedFileName))
            {
                resultProductBluetoothDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductBluetoothDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductGeneralPhoneListAsync(ResultProductGeneralPhoneDto resultProductGeneralPhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductGeneralPhoneDto.SavedFileName))
            {
                resultProductGeneralPhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductGeneralPhoneDto.SavedFileName);
            }
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

        public async Task GoogleStorageProductAdditionControlAsync(CreateProductDto createProductDto)
        {
            if (createProductDto.Photo != null)
            {
                createProductDto.SavedFileName = GenerateFileNameToSave(createProductDto.Photo.FileName);
                createProductDto.SavedUrl = await UploadFileAsync(createProductDto.Photo, createProductDto.SavedFileName);
                createProductDto.ProductImageUrl = await GetSignedUrlProductImageAsync(createProductDto.SavedFileName);
            }
        }

        public async Task<string> RefreshSignedUrlAsync(string fileNameToRead, int timeOutInMinutes)
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

        public async Task ReplaceProductPhoto(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Photo != null)
            {
                //replace the file by deleting referee.SavedFileName file and then uploading new referee.Photo
                if (!string.IsNullOrEmpty(updateProductDto.SavedFileName))
                {
                    await DeleteFileAsync(updateProductDto.SavedFileName);
                }
                updateProductDto.SavedFileName = GenerateFileNameToSave(updateProductDto.Photo.FileName);
                updateProductDto.SavedUrl = await UploadFileAsync(updateProductDto.Photo, updateProductDto.SavedFileName);
                updateProductDto.ProductImageUrl = await GetSignedUrlProductImageAsync(updateProductDto.SavedFileName);
            }
        }

        public async Task ReplaceProductImagePhoto(UpdateProductImageDto updateProductImageDto)
        {
            // 1. Büyük fotoğraf listesinin boş olmadığını kontrol et
            if (updateProductImageDto.BigPhoto != null && updateProductImageDto.BigPhoto.Any())
            {
                // 2. Kaydedilmiş dosya adları listesini kontrol et ve boş değilse dosyaları sil
                if (updateProductImageDto.BigSavedFileName != null && updateProductImageDto.BigSavedFileName.Any())
                {
                    foreach (var savedFileName in updateProductImageDto.BigSavedFileName)
                    {
                        await DeleteFileAsync(savedFileName);
                    }
                }
                if (updateProductImageDto.SmallSavedFileName != null && updateProductImageDto.SmallSavedFileName.Any())
                {
                    foreach (var savedFileName in updateProductImageDto.SmallSavedFileName)
                    {
                        await DeleteFileAsync(savedFileName.ToString());
                    }
                }
                // 3. Yeni dosya adları ve URL'ler için listeleri başlat
                updateProductImageDto.BigSavedFileName = new List<string>();
                updateProductImageDto.SmallSavedFileName = new List<string>();
                updateProductImageDto.BigSavedUrl = new List<string>();
                updateProductImageDto.SmallSavedUrl = new List<string>();
                updateProductImageDto.ProductBigImageUrl = new List<string>();
                updateProductImageDto.ProductSmallImageUrl = new List<string>();

                // 4. Her bir fotoğraf dosyasını işle
                foreach (var photo in updateProductImageDto.BigPhoto)
                {
                    // 4.1. Yeni dosya adını oluştur
                    var newFileName = GenerateFileNameToSave(photo.FileName);

                    // 4.2. Yeni dosyayı yükle ve URL'sini al
                    var newFileUrl = await UploadFileAsync(photo, newFileName);
                    var productBigImageUrl = await GetSignedUrlProductImageAsync(newFileName);

                    // 4.3. Yeni dosya adını ve URL'yi listelere ekle
                    updateProductImageDto.BigSavedFileName.Add(newFileName);
                    updateProductImageDto.BigSavedUrl.Add(newFileUrl);
                    updateProductImageDto.ProductBigImageUrl.Add(productBigImageUrl);
                }

                foreach (var photo in updateProductImageDto.SmallPhoto!)
                {
                    // 4.1. Yeni dosya adını oluştur
                    var newFileName = GenerateFileNameToSave(photo.FileName);

                    // 4.2. Yeni dosyayı yükle ve URL'sini al
                    var newFileUrl = await UploadFileAsync(photo, newFileName);
                    var productSmallImageUrl = await GetSignedUrlProductImageAsync(newFileName);

                    // 4.3. Yeni dosya adını ve URL'yi listelere ekle
                    updateProductImageDto.SmallSavedFileName.Add(newFileName);
                    updateProductImageDto.SmallSavedUrl.Add(newFileUrl);
                    updateProductImageDto.ProductSmallImageUrl.Add(productSmallImageUrl);
                }
            }
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

        public async Task GenerateSignedUrlProductİphonePhoneListAsync(ResultProductİphonePhoneDto resultProductİphonePhoneDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductİphonePhoneDto.SavedFileName))
            {
                resultProductİphonePhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductİphonePhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductSamsungPhoneListAsync(ResultProductSamsungPhoneDto resultProductSamsungPhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductSamsungPhoneDto.SavedFileName))
            {
                resultProductSamsungPhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductSamsungPhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductXiaomiPhoneListAsync(ResultProductXiaomiPhoneDto resultProductXiaomiPhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductXiaomiPhoneDto.SavedFileName))
            {
                resultProductXiaomiPhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductXiaomiPhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductRealmePhoneListAsync(ResultProductRealmePhoneDto resultProductRealmePhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductRealmePhoneDto.SavedFileName))
            {
                resultProductRealmePhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductRealmePhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductVivoPhoneListAsync(ResultProductVivoPhoneDto resultProductVivoPhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductVivoPhoneDto.SavedFileName))
            {
                resultProductVivoPhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductVivoPhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductTecnoCamonPhoneListAsync(ResultProductTecnoCamonPhoneDto resultProductTecnoCamonPhoneDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductTecnoCamonPhoneDto.SavedFileName))
            {
                resultProductTecnoCamonPhoneDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductTecnoCamonPhoneDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductSmartWatchListAsync(ResultProductSmartWatchDto resultProductSmartWatchDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductSmartWatchDto.SavedFileName))
            {
                resultProductSmartWatchDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductSmartWatchDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductMemoryCardListAsync(ResultProductMemoryCardDto resultProductMemoryCardDto)
        {
            
            if (!string.IsNullOrWhiteSpace(resultProductMemoryCardDto.SavedFileName))
            {
                resultProductMemoryCardDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductMemoryCardDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductLaptopComputerListAsync(ResultProductLaptopComputerDto resultProductLaptopComputerDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductLaptopComputerDto.SavedFileName))
            {
                resultProductLaptopComputerDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductLaptopComputerDto.SavedFileName);
            }
        }

        public async Task GenerateSignedUrlProductDesktopComputerListAsync(ResultProductDesktopComputerDto resultProductDesktopComputerDto)
        {
            if (!string.IsNullOrWhiteSpace(resultProductDesktopComputerDto.SavedFileName))
            {
                resultProductDesktopComputerDto.ProductImageUrl = await GetSignedUrlProductImageAsync(resultProductDesktopComputerDto.SavedFileName);
            }
        }
    }
}

