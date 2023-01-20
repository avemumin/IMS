using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _listOfProducts;

        public ProductRepository()
        {
            _listOfProducts = new List<Product>()
            {
                new Product() {ProductId = 1, ProductName = "Bike", Price = 150, Quantity = 10},
                new Product() {ProductId = 2, ProductName = "Car", Price = 25000, Quantity = 5},
                new Product() {ProductId = 3, ProductName = "Cat", Price = 3.41, Quantity = 22},
                new Product() {ProductId = 4, ProductName = "Dog", Price = 31, Quantity = 3},
                new Product() {ProductId = 5, ProductName = "Nic", Price = 15, Quantity = 31}

            };
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName)) return await Task.FromResult(_listOfProducts);

            return _listOfProducts.Where(x =>
                x.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }

        public Task AddProductAsync(Product product)
        {
            if (_listOfProducts.Any(x =>
                    x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var maxId = _listOfProducts.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
            _listOfProducts.Add(product);
            return Task.CompletedTask;
        }

        public Task EditProductAsync(Product product)
        {
            var getToEdit = _listOfProducts.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (getToEdit != null)
            {
                getToEdit.ProductName = product.ProductName;
                getToEdit.Price = product.Price;
                getToEdit.Quantity = product.Quantity;
            }

            return Task.CompletedTask;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var inv = _listOfProducts.First(x => x.ProductId == productId);
            var newInv = new Product()
            {
                ProductId = inv.ProductId,
                ProductName = inv.ProductName,
                Price = inv.Price,
                Quantity = inv.Quantity
            };
            return await Task.FromResult(newInv);

        }
    }
}
