using MS.API.Common;

namespace ProductMicroservices.Models
{
    public class ProductRequest
    {
        public string ProductName { get; set; }
        public long CategoryId { get; set; }
    }
    public class AllCategoryItemFromAPI
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class AllCategoryItemFromAPIResponse : GenericResponse
    {
        public List<AllCategoryItemFromAPI> Data { get; set; }
    }
}
