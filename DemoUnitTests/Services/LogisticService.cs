using DemoUnitTests.API.Interfaces;
using DemoUnitTests.API.Models;

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
            Order order = _orderService.GetById(orderId);

            for(int p=0; p < order.Lines.Count(); p++)
            {
                Order.OrderLine line = order.Lines[p];
                int stock = _stockService.GetStock(line.ProductId);
                if(line.Quantity > stock)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ValideOrder(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
