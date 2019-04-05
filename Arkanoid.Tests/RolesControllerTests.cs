using Arkanoid.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Tests
{
    [TestFixture]
    public class RolesControllerTests
    {
        public class FakeRoleManager : RoleManager<IdentityRole>
        {
            public FakeRoleManager()
                : base(new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
            { }
        }

        public class FakeUserManager : UserManager<IdentityUser>
        {
            public FakeUserManager()
                : base(new Mock<IUserStore<IdentityUser>>().Object,
                      new Mock<IOptions<IdentityOptions>>().Object,
                      new Mock<IPasswordHasher<IdentityUser>>().Object,
                      new IUserValidator<IdentityUser>[0],
                      new IPasswordValidator<IdentityUser>[0],
                      new Mock<ILookupNormalizer>().Object,
                      new Mock<IdentityErrorDescriber>().Object,
                      new Mock<IServiceProvider>().Object,
                      new Mock<ILogger<UserManager<IdentityUser>>>().Object)
            { }
        }

        Mock<FakeRoleManager> _roleManagerMock;
        Mock<FakeUserManager> _userManagerMock;
        RolesController controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _roleManagerMock = new Mock<FakeRoleManager>();
            _userManagerMock = new Mock<FakeUserManager>();
            controller = new RolesController(_roleManagerMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task Create_RedirectToIndex()
        {
            // Arrange
            _roleManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await controller.Create("role");

            // Assert
            _roleManagerMock.Verify(m => m.CreateAsync(It.IsAny<IdentityRole>()));
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

        [Test]
        public async Task Edit_RedirectToUserList()
        {
            // Arrange
            List<string> allRoles = new List<string>{};
            IdentityUser userData = new IdentityUser
            {
                Id = "1"
            };

            _userManagerMock.Setup(m => m.FindByIdAsync("name")).ReturnsAsync(userData);
            _userManagerMock.Setup(m => m.GetRolesAsync(userData)).ReturnsAsync(allRoles);

            // Act
            var result = await controller.Edit("name", allRoles);

            // Assert
            Assert.AreEqual("UserList", (result as RedirectToActionResult).ActionName);
        }

        [Test]
        public async Task Delete_RedirectToIndex()
        {
            // Arrange
            IdentityRole role = new IdentityRole { Id = "roleId" };

            _roleManagerMock.Setup(m => m.FindByIdAsync("role")).ReturnsAsync(role);

            // Act
            var result = await controller.Delete("role");

            // Assert
            _roleManagerMock.Verify(m => m.DeleteAsync(role));
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
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
    }
}