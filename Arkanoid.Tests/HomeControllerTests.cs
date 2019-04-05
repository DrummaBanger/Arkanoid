using Arkanoid.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Arkanoid.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        HomeController controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            controller = new HomeController();
        }

        [Test]
        public void Index_ReturnView()
        {
            // Arrange

            // Act
            var result = controller.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.NotNull(result as ViewResult);
            Assert.AreEqual("Index", (result as ViewResult).ViewName);
        }

        [Test]
        public void Error_ReturnView()
        {
            // Arrange

            // Act
            var result = controller.Error(404);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.AreEqual("NotFound", (result as ViewResult).ViewName);
        }
    }
}