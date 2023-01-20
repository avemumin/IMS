using IMS.CoreBusiness;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IEditProductUseCase
    {
        Task ExecuteExitAsync(Product product);
    }
}
