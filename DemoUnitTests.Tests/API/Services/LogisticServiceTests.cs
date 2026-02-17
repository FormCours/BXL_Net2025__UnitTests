using DemoUnitTests.API.Interfaces;
using DemoUnitTests.API.Models;
using DemoUnitTests.API.Services;

namespace DemoUnitTests.Tests.API.Services
{
    #region Fake
    internal class FakeOrderService : IOrderService
    {
        public Order CreateNewOrder()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int orderId)
        {
            if (orderId != 42) 
                throw new ArgumentException("Bad Order !");

            return new Order()
            {
                Id = 42,
                IsReadyForShipping = false,
                Lines = [
                    new Order.OrderLine() { ProductId = 1, Quantity = 10 },
                    new Order.OrderLine() { ProductId = 3, Quantity = 2 },
                    new Order.OrderLine() { ProductId = 4, Quantity = 1 },
                ]
            };
        }
    }

    internal class FakeStockService_With10UnitByProduct : IStockService
    {
        public int GetStock(int productId)
        {
            return 10;
        }

        public void RemoveStock(int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public class LogisticServiceTests
    {
        // Exemple avec des dépendences simulée
        [Fact]
        public void PrepareOrder_ProductOrderedInStock_ReturnTrue()
        {
            // Simulation des dépendences
            IOrderService stubOrderService = new FakeOrderService();
            IStockService stubStockService = new FakeStockService_With10UnitByProduct();

            // Arrange
            ILogisticService logisticService = new LogisticService(stubOrderService, stubStockService);
            int orderId = 42;

            // Act
            bool result = logisticService.PrepareOrder(orderId);

            //Assert
            Assert.True(result);
        }

    }
}
