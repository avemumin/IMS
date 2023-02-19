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

        public Task UpdateProductAsync(Product product)
        {
            var getToEdit = _listOfProducts.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (getToEdit != null)
            {
                getToEdit.ProductName = product.ProductName;
                getToEdit.Price = product.Price;
                getToEdit.Quantity = product.Quantity;
                getToEdit.ProductInventories = product.ProductInventories;
            }

            return Task.CompletedTask;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var prod = _listOfProducts.FirstOrDefault(x => x.ProductId == productId);
            var newProd = new Product();
            if (prod != null)
            {
                newProd.ProductId = prod.ProductId;
                newProd.ProductName = prod.ProductName;
                newProd.Price = prod.Price;
                newProd.Quantity = prod.Quantity;
                newProd.ProductInventories = new List<ProductInventory>();
                if (prod.ProductInventories != null && prod.ProductInventories.Count > 0)
                {
                    foreach (var prodInv in prod.ProductInventories)
                    {
                        var newProdInv = new ProductInventory()
                        {
                            InventoryId = prodInv.InventoryId,
                            ProductId = prodInv.ProductId,
                            Product = prod,
                            Inventory = new Inventory(),
                            InventoryQuantity = prodInv.InventoryQuantity
                        };
                        if (prodInv.Inventory != null)
                        {
                            newProdInv.Inventory.InventoryId = prodInv.Inventory.InventoryId;
                            newProdInv.Inventory.InventoryName = newProdInv.Inventory.InventoryName;
                            newProdInv.Inventory.Price = newProdInv.Inventory.Price;
                            newProdInv.Inventory.Quantity = newProdInv.Inventory.Quantity;
                        }
                        newProd.ProductInventories.Add(newProdInv);
                    }
                }
            };
            return await Task.FromResult(newProd);

        }
    }
}
