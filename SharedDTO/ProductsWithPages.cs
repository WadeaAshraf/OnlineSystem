

namespace SharedDTO
{
    public class ProductsWithPages
    {
        public List<CategoryProductDTO> LstCategoryProductsDTO { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }
}
