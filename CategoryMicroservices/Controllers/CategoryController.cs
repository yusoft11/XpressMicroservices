using CategoryMicroservices.Models;
using CategoryMicroservices.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryMicroservices.Controllers
{
    [Route("api/categoryRequests")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly string conn;
        private readonly ICategoryService _catServices;
        public CategoryController(IConfiguration config, ICategoryService catServices)
        {
            _config = config;
            _catServices = catServices;
            conn = config.GetConnectionString("CategoryConn");
        }
        [HttpGet]
        public GetAllCatResponse Index()
        {
            var resp = _catServices.GetAllCategories(conn);
            return resp;
        }
        [HttpPost("create")]
        public async Task<CategoryInitResponse> Create(CategoryRequest req)
        {
            var resp = await _catServices.CreateCat(req, conn);
            return resp;
        }
        [HttpGet("getallproductsbycatid/{categoryId}")]
        public async Task<CatProdItemFrmAPIResponse> GetProducts(long categoryId)
        {
            var resp = await _catServices.GetProdByCatID(categoryId);
            return resp;
        }

    }
}
