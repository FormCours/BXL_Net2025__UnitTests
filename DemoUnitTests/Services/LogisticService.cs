using DemoUnitTests.API.Interfaces;

namespace DemoUnitTests.API.Services
{
    public class LogisticService : ILogisticService
    {
        private readonly IOrderService _orderService;
        private readonly IStockService _stockService;

        public LogisticService(IOrderService orderService, IStockService stockService)
        {
            _orderService = orderService;
            _stockService = stockService;
        }


        public bool PrepareOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool ValideOrder(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
