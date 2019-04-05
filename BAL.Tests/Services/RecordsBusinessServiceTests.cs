using Moq;
using NUnit.Framework;
using BAL.Models;
using BAL.Services;
using DAL.Models;
using DAL.Services;
using BAL.Services.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Tests
{
    [TestFixture]
    public class RecordsBusinessServiceTests
    {
        Mock<IRecordsDataService> recordsDataService;
        RecordsBusinessService recordsService;
        RecordsData modelData;
        RecordsBusiness modelBusiness;

        [SetUp]
        public void Setup()
        {
            // Arrange
            modelData = new RecordsData { UserID = "1", RecordID = 1, UserName = "name", UserScore = 1 };
            modelBusiness = new RecordsBusiness { UserID = "1", RecordID = 1, UserName = "name", UserScore = 1 };

            recordsDataService = new Mock<IRecordsDataService>();

            recordsService = new RecordsBusinessService(recordsDataService.Object);
        }

        private List<RecordsData> GetTestRecords()
        {
            var records = new List<RecordsData>
            {
                new RecordsData { UserID = "1", RecordID = 1, UserName = "name", UserScore = 1 },
                new RecordsData { UserID = "2", RecordID = 2, UserName = "name1", UserScore = 2 },
                new RecordsData { UserID = "3", RecordID = 3, UserName = "name2", UserScore = 3 },

            };
            return records;
        }

        [Test]
        public void DeletePersonsAsync_ReturnTaskOfBool()
        {
            // Arrange

            // Act
            var result = recordsService.DeleteRecordsAsync(100);

            // Assert
            recordsDataService.Verify(m => m.DeleteAsync(100));
            Assert.That(result, Is.TypeOf<Task<bool>>());
        }

        [Test]
        public async Task GetPersons_ReturnListOfPersonsBusinessModel()
        {
            // Arrange
            recordsDataService.Setup(m => m.GetRecords("User")).ReturnsAsync(GetTestRecords());

            RecordsBusinessService _personsService = new RecordsBusinessService(recordsDataService.Object);

            // Act
            var result = await _personsService.GetRecordsAsync("User");

            // Assert
            Assert.AreEqual(GetTestRecords().Count, result.Count);
            Assert.That(result, Is.TypeOf<List<RecordsBusiness>>());
        }

        [Test]
        public async Task GetDetails_ReturnPersonsBusinessModel()
        {
            // Arrange
            recordsDataService.Setup(m => m.GetDetails(100)).ReturnsAsync(modelData);

            RecordsBusinessService _personsService = new RecordsBusinessService(recordsDataService.Object);

            // Act
            var result = await _personsService.GetDetails(100);

            // Assert
            Assert.That(result, Is.TypeOf<RecordsBusiness>());
        }
    }
}
