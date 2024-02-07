using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservices.Models;
using ProductMicroservices.Service;

namespace ProductMicroservices.Controllers
{
    [Route("api/productRequests")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly string conn;
        private readonly IProductService _prodServices;
        public ProductController(IConfiguration config, IProductService prodServices)
        {
            _config = config;
            _prodServices = prodServices;
            conn = config.GetConnectionString("ProductConn");
        }
        [HttpGet("getallcategories")]
        public async Task<AllCategoryItemFromAPIResponse> Index()
        {
            var resp = await _prodServices.GetAllCategories();
            return resp;
        }
    }
}
