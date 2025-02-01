using Limupa.DtoLayer.ProductDtos;


namespace Limupa.UI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await httpClient.PostAsJsonAsync("products", createProductDto);
        }

        public async Task DeleteProductAsync(string id)
        {
            await httpClient.DeleteAsync("products/" + id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await httpClient.GetAsync("products");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }
        public async Task<List<ResultProductLastTenDto>> GetLastTenProductAsync()
        {
            var responseMessage = httpClient.GetFromJsonAsync<List<ResultProductLastTenDto>>("products/GetLastTenProduct").GetAwaiter().GetResult();
            return responseMessage;
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("products/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDto>();
            return value;
        }
        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/ProductWithCategoryList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryByIdDto>> GetProductWithCategoryByIdAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("products/ProductWithCategoryList/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryByIdDto>>();
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await httpClient.PutAsJsonAsync("products", updateProductDto);
        }

        public async Task<List<ResultTenDataByProductPersonelCareCategoryDto>> GetTenDataByProductPersonelCareCategoryAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetTenDataByProductPersonelCareCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultTenDataByProductPersonelCareCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductByElectronicDeviceCategoryDto>> GetProductByElectronicDeviceCategoryAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductByElectronicDeviceCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductByElectronicDeviceCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductByHomeAndEntertainmentDeviceCategoryDto>> GetProductByHomeAndEntertainmentDeviceCategoryAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductByHomeAndEntertainmentDeviceCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductByHomeAndEntertainmentDeviceCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductİpohnePhoneModelDto>> GetProductİpohnePhoneModelAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductİpohnePhoneModel");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductİpohnePhoneModelDto>>();
            return values;
        }

        public async Task<List<ResultProductMackbookLaptopModelDto>> GetProductMackbookLaptopModelAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductMackbookLaptopModel");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductMackbookLaptopModelDto>>();
            return values;
        }

        public async Task<List<ResultProductVideoGameModelDto>> GetProductVideoGameModelAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductVideoGameModel");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductVideoGameModelDto>>();
            return values;
        }

        public async Task<List<ResultProductBluetoothDto>> GetProductBluetoothListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel)
        {
            
            string requestUri = $"products/ProductBluetoothListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count -1)
                {
                    requestUri += "&";
                }
            }

            // Fiyat aralıkları
            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductBluetoothDto>>();
                return values;
            }
            else
            {
                
                return null;
            }
        }

        public async Task<List<ResultProductBluetoothDto>> GetProductBluetoothListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductBluetoothList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductBluetoothDto>>();
            return values;
        }


        public async Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            
            string requestUri = $"products/GetProductGeneralPhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductGeneralPhoneDto>>();
                return values;
            }
            else
            {
                
                return null;
            }
        }

        public async Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductGeneralPhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductGeneralPhoneDto>>();
            return values;

        }

        public async Task<List<ResultProductİpohnePhoneModelDto>> GetProductİphonePhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductİphonePhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductİpohnePhoneModelDto>>();
            return values;
        }

        public async Task<List<ResultProductİpohnePhoneModelDto>> GetProductİphonePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize)
        {
            
            string requestUri = $"products/GetProductİphonePhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductİpohnePhoneModelDto>>();
                return values;
            }
            else
            {
                
                return null;
            }
        }

        public async Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductSamsungPhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductSamsungPhoneDto>>();
            return values;
        }

        public async Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            
            string requestUri = $"products/GetProductSamsungPhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductSamsungPhoneDto>>();
                return values;
            }
            else
            {               
                return null;
            }
        }

        public async Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductXiaomiPhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductXiaomiPhoneDto>>();
            return values;
        }

        public async Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            string requestUri = $"products/GetProductXiaomiPhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductXiaomiPhoneDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductRealmePhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductRealmePhoneDto>>();
            return values;
        }

        public async Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            string requestUri = $"products/GetProductRealmePhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductRealmePhoneDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductVivoPhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductVivoPhoneDto>>();
            return values;
        }

        public async Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            string requestUri = $"products/GetProductVivoPhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductVivoPhoneDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductTecnoCamonPhoneList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductTecnoCamonPhoneDto>>();
            return values;
        }

        public async Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            string requestUri = $"products/GetProductTecnoCamonPhoneListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"productModel={productModel[i]}";

                if (i < productModel.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"&productPrice={productPrice[i]}";
            }

            for (int i = 0; i < productInternalMemorySize.Count; i++)
            {
                requestUri += $"&productInternalMemorySize={productInternalMemorySize[i]}";
            }

            for (int i = 0; i < productMobileRamSize.Count; i++)
            {
                requestUri += $"&productMobileRamSize={productMobileRamSize[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductTecnoCamonPhoneDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductSmartWatchList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductSmartWatchDto>>();
            return values;
        }

        public async Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productColor)
        {
            string requestUri = $"products/GetProductSmartWatchListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"productPrice={productPrice[i]}";

                if (i < productPrice.Count - 1)
                {
                    requestUri += "&";
                }
            }

            // Fiyat aralıkları
            for (int i = 0; i < productColor.Count; i++)
            {
                requestUri += $"&productColor={productColor[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductSmartWatchDto>>();
                return values;
            }
            else
            {

                return null;
            }
        }

        public async Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductMemoryCardList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductMemoryCardDto>>();
            return values;
        }

        public async Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productStorage)
        {
            string requestUri = $"products/GetProductMemoryCardListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"productPrice={productPrice[i]}";

                if (i < productPrice.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productModel.Count; i++)
            {
                requestUri += $"&productModel={productModel[i]}";
            }

            for (int i = 0; i < productStorage.Count; i++)
            {
                requestUri += $"&productStorage={productStorage[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductMemoryCardDto>>();
                return values;
            }
            else
            {

                return null;
            }
        }

        public async Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductLaptopComputerList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductLaptopComputerDto>>();
            return values;
        }

        public async Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam)
        {
            string requestUri = $"products/GetProductLaptopComputerListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"productPrice={productPrice[i]}";

                if (i < productPrice.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productProcessModel.Count; i++)
            {
                requestUri += $"&productProcessModel={productProcessModel[i]}";
            }

            for (int i = 0; i < productGrapichCardModel.Count; i++)
            {
                requestUri += $"&productGrapichCardModel={productGrapichCardModel[i]}";
            }

            for (int i = 0; i < productMemoryRam.Count; i++)
            {
                requestUri += $"&productMemoryRam={productMemoryRam[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductLaptopComputerDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListAsync()
        {
            var responseMessage = await httpClient.GetAsync("products/GetProductDesktopComputerList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDesktopComputerDto>>();
            return values;
        }

        public async Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam)
        {
            string requestUri = $"products/GetProductDesktopComputerListFilter?";

            // Filtreler
            for (int i = 0; i < productName.Count; i++)
            {
                requestUri += $"productName={productName[i]}";

                if (i < productName.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productPrice.Count; i++)
            {
                requestUri += $"productPrice={productPrice[i]}";

                if (i < productPrice.Count - 1)
                {
                    requestUri += "&";
                }
            }

            for (int i = 0; i < productProcessModel.Count; i++)
            {
                requestUri += $"&productProcessModel={productProcessModel[i]}";
            }

            for (int i = 0; i < productGrapichCardModel.Count; i++)
            {
                requestUri += $"&productGrapichCardModel={productGrapichCardModel[i]}";
            }

            for (int i = 0; i < productMemoryRam.Count; i++)
            {
                requestUri += $"&productMemoryRam={productMemoryRam[i]}";
            }

            var responseMessage = await httpClient.GetAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDesktopComputerDto>>();
                return values;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetSearchProductAsync(string productCategory, string productTextSearch)
        {
            var responseMessage = await httpClient.GetAsync($"products/GetSearchProduct?productCategory={productCategory}&productTextSearch={productTextSearch}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }
    }
}
