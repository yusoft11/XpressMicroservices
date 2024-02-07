using Dapper;
using MS.API.Common;
using ProductMicroservices.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductMicroservices.Service
{
    public interface IProductService
    {
        Task<AllCategoryItemFromAPIResponse> GetAllCategories();
        Task<ProductInitResponse> CreateProd(ProductRequest req, string conn);
        ProductItemResponse GetProdByCategoryId(long CategoryId, string conn);
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
                return new AllCategoryItemFromAPIResponse() { Data = new List<AllCategoryItemFromAPI>(), Message = "Error occurred", Code = "96" };
            }
            return new AllCategoryItemFromAPIResponse() { Data = _resp.Data, Message = "Operation successful", Code = "00" };
        }

        public async Task<ProductInitResponse> CreateProd(ProductRequest req, string conn)
        {
            try
            {
                IDbConnection con = new SqlConnection(conn);
                var isDataExist = IsProdNameExist(req.ProductName, con);
                if(isDataExist == null)
                {
                    string sql = "insert into products (productname,categoryid) values(@P1,@P2)";
                    await con.ExecuteAsync(sql, new { P1 = req.ProductName, P2 = req.CategoryId });

                }
                else
                {
                    return new ProductInitResponse() { Message = "Duplicate name", Code = "96" };
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new ProductInitResponse() { Message = "Error occurred", Code = "96" };
            }
            return new ProductInitResponse() { Message = "Operation successful", Code = "00" };
        }
        public ProductItemResponse GetProdByCategoryId(long CategoryId, string conn)
        {
            ProductItemResponse resp = new ProductItemResponse() { Data = new List<ProductItem>() };
            try
            {
                IDbConnection con = new SqlConnection(conn);
                var _items = GetProdByCatId(CategoryId, con);
                if(_items !=null && _items.Count > 0)
                {

                    resp.Data = _items;
                }
                else
                {
                    return new ProductItemResponse() { Data = new List<ProductItem>(), Message = "No record found", Code = "96" };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new ProductItemResponse() {Data = new List<ProductItem>(), Message = "Error occurred", Code = "96" };
            }
            return new ProductItemResponse() { Data = resp.Data, Message = "Operation successful", Code = "00" };
        }

        private ProductCheckItem IsProdNameExist(string _prodname, IDbConnection con) => con.Query<ProductCheckItem>($@"select ProductName from products where productname='{_prodname}'").FirstOrDefault();
        private List<ProductItem> GetProdByCatId(long _catId, IDbConnection con) => con.Query<ProductItem>($@"select categoryid, productname from products where categoryid={_catId}").ToList();
    }
}
