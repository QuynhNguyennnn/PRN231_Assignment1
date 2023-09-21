namespace eStoreAPI.DTOs
{
    public class AddProductDtos
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitslnStock { get; set; }
    }
}
