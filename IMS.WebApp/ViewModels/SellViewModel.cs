using IMS.CoreBusiness;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        public string SalesOrderNumber { get; set; }
        public int ProductId { get; set; }
        public string QuantityToSell { get; set; }
        public double UnitPrice { get; set; }
        public Product? Product { get; set; }
    }
}
