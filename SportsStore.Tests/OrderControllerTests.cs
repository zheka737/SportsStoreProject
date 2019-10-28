using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{

    public class OrderControllerTests
    {

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - create mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = target.Checkout(order) as ViewResult;
            //mock.Object.SaveOrder(new Order());
            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert - check that the order hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_SubmitOrder()
        {
            //Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            //Act
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            //Assert
            mock.Verify(e => e.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);

        }

        [Fact]
        public void TestName()
        {
            Mock<TestClass1> mock = new Mock<TestClass1>();
            mock.Object.testMethod("fff");

            mock.Verify(e => e.testMethod(It.Is<string>(y => y == "fff")));
        }

        [Fact]
        public void TestName2()
        {
            Mock<IFoo> mock = new Mock<IFoo>();
            // mock.Setup(e => e.DoSomething(It.IsIn<string>(new string[] {"ping", "ping2"}))).Returns(true);
            // mock.Setup(e => e.DoSomething(It.IsIn<string>(new string[] {"ping3"}))).Returns(true);
            // Assert.True(mock.Object.DoSomething("ping4"));
        
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>())).Returns((string s) => s.ToUpper());

            var a = mock.Object.DoSomethingStringy("ddd");
        }

    }

    public class TestClass1
    {
        public virtual void testMethod(string par)
        {
            System.Console.WriteLine();
        }
    }

    public interface IFoo
    {
        Bar Bar { get; set; }
        string Name { get; set; }
        int Value { get; set; }
        bool DoSomething(string value);
        bool DoSomething(int number, string value);
        string DoSomethingStringy(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int value);
    }

    public class Bar
    {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz
    {
        public virtual string Name { get; set; }
    }


}