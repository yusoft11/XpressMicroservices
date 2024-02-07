using MS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace ProductMicroservices.Models
{
    public class ProductRequest
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
    public class ProductInitResponse : GenericResponse
    {

    }
    public class ProductCheckItem
    {
        public string ProductName { get; set; }
    }
    public class ProductItem
    {
        public long CategoryId { get; set;}
        public string ProductName { get; set; }
    }
    public class ProductItemResponse : GenericResponse
    {
        public List<ProductItem> Data { get; set; }
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
