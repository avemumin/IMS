using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModels;

namespace IMS.WebApp.ViewModelsValidations
{
    public class Produce_EnsureEnoughtInventoryQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;

            if (produceViewModel != null)
            {
                if (produceViewModel.Product != null && produceViewModel.Product.ProductInventories != null)
                {
                    foreach (var pi in produceViewModel.Product.ProductInventories)
                    {
                        if (pi.Inventory != null && pi.InventoryQuantity * produceViewModel.QunatityToProduct >
                            pi.Inventory.Quantity)
                        {
                            return new ValidationResult($"The inventory ({pi.Inventory.InventoryName}) is not enough to produce {produceViewModel.QunatityToProduct} products",
                                new[] { validationContext.MemberName });
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
