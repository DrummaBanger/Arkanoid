using Arkanoid.Controllers;
using Arkanoid.Models;
using Arkanoid.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Arkanoid.Tests
{
    [TestFixture]
    public class RecordsControllerTests
    {
        RecordsController controller;
        RecordsController RecordsControllerContext;
        Mock<IRecordsService> records;
        Records recordsModel;

        [SetUp]
        public void Setup()
        {
            // Arrange
            recordsModel = new Records { UserID = "1", RecordID = 1, UserName = "name", UserScore = 1 };

            records = new Mock<IRecordsService>();

            controller = new RecordsController(records.Object);

            RecordsControllerContext = new RecordsController(records.Object);
            RecordsControllerContext.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "User")
                    }, "someAuthTypeName"))
                }
            };
        }

        private List<Records> GetTestRecords()
        {
            var records = new List<Records>
            {
                new Records { UserID = "1", RecordID = 1, UserName = "name", UserScore = 1 },
                new Records { UserID = "2", RecordID = 2, UserName = "name1", UserScore = 2 },
            };
            return records;
        }

        [Test]
        public async Task Index_GetTestPersons_ReturnViewResult()
        {
            // Arrange
            records.Setup(m => m.GetRecords("UserName")).ReturnsAsync(GetTestRecords());

            var contr = new RecordsController(records.Object);
            contr.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "UserName")
                    }, "someAuthTypeName"))
                }
            };

            // Act
            var result = await contr.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.IsAssignableFrom<List<Records>>((result as ViewResult).Model);
            Assert.AreEqual(GetTestRecords().Count, ((result as ViewResult).Model as List<Records>).Count);
            Assert.AreEqual("name1", ((result as ViewResult).Model as List<Records>)[1].UserName);
        }

        [Test]
        public async Task Index_ReturnPersons()
        {
            // Arrange

            // Act
            var result = await RecordsControllerContext.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.IsAssignableFrom<Records[]>((result as ViewResult).Model);
        }

        [Test]
        public async Task Details_NullId_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Details(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_ModelStateIsValid_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await RecordsControllerContext.Create(recordsModel);

            //Assert
            records.Verify(m => m.CreateRecord(recordsModel));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Edit_NullId_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Edit(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_ModelStateIsValid_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await RecordsControllerContext.Edit(recordsModel.RecordID, recordsModel);

            //Assert
            records.Verify(m => m.UpdateRecord(recordsModel));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Delete_NullId_ReturnNotFoundResult()
        {
            // Arrange

            // Act
            var result = await controller.Delete(null);

            // Arrange
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteConfirmed_ReturnRedirectToAction()
        {
            // Arrange

            //Act
            var result = await controller.DeleteConfirmed(1);

            //Assert
            records.Verify(m => m.DeleteRecordsAsync(1));
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }
    }
}