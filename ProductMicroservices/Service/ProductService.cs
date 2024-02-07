using MS.API.Common;
using ProductMicroservices.Models;

namespace ProductMicroservices.Service
{
    public interface IProductService
    {
        Task<AllCategoryItemFromAPIResponse> GetAllCategories();
    }
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMicroServiceManager _mgrService;
        public ProductService(ILogger<ProductService> logger, IMicroServiceManager mgrService)
        {
            _logger = logger;
            _mgrService = mgrService;
        }

        public async Task<AllCategoryItemFromAPIResponse> GetAllCategories()
        {
            AllCategoryItemFromAPIResponse _resp = new AllCategoryItemFromAPIResponse { Data = new List<AllCategoryItemFromAPI>() };
            try
            {
                string sToken = "";
                var _TokenItem = await _mgrService.ValidateAuthTokenAsync();
                if(_TokenItem !=null && _TokenItem.message.ToLower() == "operation successful")
                {
                    sToken = _TokenItem.token;
                }
                else
                {
                    return new AllCategoryItemFromAPIResponse() { Data = new List<AllCategoryItemFromAPI>(), Message = "Unable to connect to the service", Code = "96"};
                }
                var _catItems = await _mgrService.GetAllCategories(sToken);
                if(_catItems.code == "00")
                {
                    if(_catItems.data !=null && _catItems.data.Any())
                    {
                        foreach(var catItem in _catItems.data)
                        {
                            _resp.Data.Add(new AllCategoryItemFromAPI
                            {
                                CategoryId = catItem.categoryId,
                                CategoryName = catItem.categoryName
                            });
                        }
                    }
                    else
                    {
                        return new AllCategoryItemFromAPIResponse() { Data = new List<AllCategoryItemFromAPI>(), Message = "No record found", Code = "96" };
                    }
                }
                else
                {
                    return new AllCategoryItemFromAPIResponse() { Data = new List<AllCategoryItemFromAPI>(), Message = "Connection not successful", Code = "96" };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
            }
            return new AllCategoryItemFromAPIResponse() { Data = _resp.Data, Message = "Operation successful", Code = "00" };
        }
    }
}
