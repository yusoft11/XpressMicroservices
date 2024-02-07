using MS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace CategoryMicroservices.Models
{
    public class CategoryRequest
    {
        [Required]
        public string? CategoryName { get; set; }
    }

    public class CategoryInitResponse : GenericResponse
    {

    }
    public class CategoryItem
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class GetAllCatResponse : GenericResponse
    {
        public List<CategoryItem> Data { get; set; }
    }

    public class CatProdItemFrmAPI
    {
        public long CategoryId { get; set; }
        public string ProductName { get; set; }
    }
    public class CatProdItemFrmAPIResponse : GenericResponse
    {
        public List<CatProdItemFrmAPI> Data { get; set; }
    }
    
}
