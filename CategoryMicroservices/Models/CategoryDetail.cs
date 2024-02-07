using MS.API.Common;

namespace CategoryMicroservices.Models
{
    public class CategoryRequest
    {
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
}
