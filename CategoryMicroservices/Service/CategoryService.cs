using CategoryMicroservices.Models;
using Dapper;
using MS.API.Common;
using System.Data;
using System.Data.SqlClient;

namespace CategoryMicroservices.Service
{

    public interface ICategoryService
    {
        Task<CategoryInitResponse> CreateCat(CategoryRequest req, string conn);
        GetAllCatResponse GetAllCategories(string conn);
        Task<CatProdItemFrmAPIResponse> GetProdByCatID(long CategoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IMicroServiceManager _mgrService;
        public CategoryService(ILogger<CategoryService> logger, IMicroServiceManager mgrService)
        {
            _logger = logger;
            _mgrService = mgrService;
    }
        public async Task<CategoryInitResponse> CreateCat(CategoryRequest req, string conn)
        {
            try
            {
                IDbConnection con = new SqlConnection(conn);
                var isDataExist = IsCategoryExist(req.CategoryName, con);
                if (isDataExist == null)
                {
                    string sql = "insert into categories (categoryname) values(@P1)";
                    await con.ExecuteAsync(sql, new { P1 = req.CategoryName });
                }
                else
                {
                    return new CategoryInitResponse() { Message = "Duplicate name", Code = "96" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new CategoryInitResponse() { Message = "Error occurred, please contact the administrator", Code = "96" };
            }
            return new CategoryInitResponse() { Message = "Operation successful", Code = "00" };
        }

        public GetAllCatResponse GetAllCategories(string conn)
        {
            GetAllCatResponse _resp = new GetAllCatResponse { Data = new List<CategoryItem>() };
            try
            {
                IDbConnection con = new SqlConnection(conn);
                var _catItems = GetAllCat(con);
                if(_catItems !=null && _catItems.Count > 0)
                {
                    _resp.Data = _catItems;
                }
                else
                {
                    return new GetAllCatResponse() { Data = _resp.Data,  Message = "Not record found", Code = "96" };
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new GetAllCatResponse() { Data = new List<CategoryItem>(), Message = "Error occurred", Code = "96" };
            }
            return new GetAllCatResponse() { Data = _resp.Data, Message = "Operation successful", Code = "00" };
        }

        public async Task<CatProdItemFrmAPIResponse> GetProdByCatID(long CategoryId)
        {
            CatProdItemFrmAPIResponse _resp = new CatProdItemFrmAPIResponse { Data = new List<CatProdItemFrmAPI>() };
            try
            {
                string sToken = "";
                var _TokenItem = await _mgrService.ValidateAuthProdTokenAsync();
                if (_TokenItem != null && _TokenItem.message.ToLower() == "operation successful")
                {
                    sToken = _TokenItem.token;
                }
                else
                {
                    return new CatProdItemFrmAPIResponse() { Data = new List<CatProdItemFrmAPI>(), Message = "Unable to connect to the service", Code = "96" };
                }
                var _catItems = await _mgrService.GetProdByCatID(sToken, CategoryId);
                if (_catItems.code == "00")
                {
                    if (_catItems.data != null && _catItems.data.Any())
                    {
                        foreach (var catItem in _catItems.data)
                        {
                            _resp.Data.Add(new CatProdItemFrmAPI
                            {
                                CategoryId = catItem.categoryId,
                                ProductName = catItem.productName
                            });
                        }
                    }
                    else
                    {
                        return new CatProdItemFrmAPIResponse() { Data = new List<CatProdItemFrmAPI>(), Message = "No record found", Code = "96" };
                    }
                }
                else
                {
                    return new CatProdItemFrmAPIResponse() { Data = new List<CatProdItemFrmAPI>(), Message = "Connection not successful", Code = "96" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return new CatProdItemFrmAPIResponse() { Data = new List<CatProdItemFrmAPI>(), Message = "Error occurred", Code = "96" };
            }
            return new CatProdItemFrmAPIResponse() { Data = _resp.Data, Message = "Operation successful", Code = "00" };
        }
        private CategoryItem? IsCategoryExist(string _catname, IDbConnection con) => con.Query<CategoryItem>($@"select * from categories where categoryname='{_catname}'").FirstOrDefault();

        private List<CategoryItem> GetAllCat(IDbConnection con) => con.Query<CategoryItem>($@"select categoryid [CategoryId], categoryname [CategoryName] from categories").ToList();
    }
}
