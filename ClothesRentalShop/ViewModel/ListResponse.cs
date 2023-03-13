namespace ClothesRentalShop.ViewModel
{
    public class PagingResponse 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }

    }
}
