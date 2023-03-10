using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);

        Task<Product?> GetProductByIdAsync(int productId);

    }
}
