using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.API.Common
{
    public class AuthTokenResponse
    {
        public string token { get; set; }
        public string message { get; set; }
    }
    public class AuthRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class CategoryAPIItem 
    {
        public long categoryId { get; set; }
        public string categoryName { get; set; }
    }
    public class CategoryAPIItemResponse : GenericResponseApi
    {
        public List<CategoryAPIItem> data { get; set; }
    }
    public class ProdCatAPIItem
    {
        public long categoryId { get; set; }
        public string productName { get; set; }
    }
    public class ProdCatAPIItemResponse : GenericResponseApi
    {
        public List<ProdCatAPIItem> data { get; set; }
    }
}
