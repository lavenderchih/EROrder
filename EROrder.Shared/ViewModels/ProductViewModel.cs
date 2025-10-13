namespace EROrder.Shared.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductListViewModel
    {
        public List<ProductViewModel> Products { get; set; } = [];
        public string PageTitle { get; set; } = string.Empty;
        public int TotalCount { get; set; }
    }
}
