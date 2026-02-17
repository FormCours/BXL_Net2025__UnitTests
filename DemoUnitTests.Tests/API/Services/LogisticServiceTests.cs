using DemoUnitTests.API.Interfaces;
using DemoUnitTests.API.Models;
using DemoUnitTests.API.Services;
using Moq;

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

    internal class FakeStockService_OnlyOneProductInStock : IStockService
    {
        public int GetStock(int productId)
        {
            return (productId == 1) ? 100 : 0;
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

        [Fact]
        public void PrepareOrder_ProductOrderedNotInStock_ReturnFalse()
        {
            // Simulation des dépendences
            IOrderService stubOrderService = new FakeOrderService();
            IStockService stubStockService = new FakeStockService_OnlyOneProductInStock();

            // Arrange
            ILogisticService logisticService = new LogisticService(stubOrderService, stubStockService);
            int orderId = 42;

            // Act
            bool result = logisticService.PrepareOrder(orderId);

            //Assert
            Assert.False(result);
        }


        // Exemple avec des dépendences simulée via le package « Moq »
        [Fact]
        public void PrepareOrder_ProductOrderedInStock_AllProductStockIsChecked()
        {
            // Mocking
            IOrderService stubOrderService = new FakeOrderService();
            Mock<IStockService> mockStockService = new Mock<IStockService>();
            mockStockService.Setup(service => service.GetStock(It.IsAny<int>())).Returns(1_000);
            // → It.IsAny<int>() : N'import quelle entier

            // Arrange
            ILogisticService logisticService = new LogisticService(stubOrderService, mockStockService.Object);

            // Act
            logisticService.PrepareOrder(42);

            // Assert
            mockStockService.Verify(service => service.GetStock(It.IsAny<int>()), Times.Exactly(3));
            // → L'order 42 obtenir via le stubOrderService, contient 3 produits !
        }

    }
}
