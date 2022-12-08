namespace Web.ViewModels.Product
{
    public class ProductFilterIndexVM
    {
        public Core.Entities.ProductCategory ProductCategory { get; set; }
        public List<Core.Entities.Product> Products { get; set; }
    }
}
