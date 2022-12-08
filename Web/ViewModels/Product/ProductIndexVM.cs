namespace Web.ViewModels.Product
{
    public class ProductIndexVM
    {
        public List<Core.Entities.ProductCategory> ProductCategories { get; set; }
        public List<Core.Entities.Product> Products { get; set; }

        public string? Title { get; set; }
    }
}
