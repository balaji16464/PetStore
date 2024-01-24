using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using static PetStoreApp.PetService;


namespace PetStoreApp.Tests
{
    [TestClass]
    public class PetServiceTests
    {
        [TestMethod]
        public async Task GetAvailablePets_Success()
        {
            // Arrange
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();

            // Mock the GetAsync method for fetching available pets
            httpClientWrapperMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[{\"id\":1,\"name\":\"Pet1\",\"status\":\"available\",\"category\":{\"id\":1,\"name\":\"Category1\"}}, {\"id\":2,\"name\":\"Pet2\",\"status\":\"available\",\"category\":{\"id\":2,\"name\":\"Category2\"}}]"),
                });
            var petService = new PetService(httpClientWrapperMock.Object);

            // Act
            var result = await petService.DisplayAvailablePets();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Pet1", result[0].Name);
            Assert.AreEqual("available", result[0].Status);
            Assert.AreEqual(1, result[0].Category.Id);
            Assert.AreEqual("Category1", result[0].Category.Name);

            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Pet2", result[1].Name);
            Assert.AreEqual("available", result[1].Status);
            Assert.AreEqual(2, result[1].Category.Id);
            Assert.AreEqual("Category2", result[1].Category.Name);
        }
    }
}
