using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetStoreApp;
using System.Net.Http.Formatting;

namespace PetStore.tests
{
    [TestClass]
    public class PetServiceTests
    {
        [TestMethod]
        public async Task GetAvailablePets_Success()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClientWrapper>();
            httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Dictionary<string, dynamic>>(
                        new Dictionary<string, dynamic>
                        {
                            ["paths"] = new Dictionary<string, dynamic>
                            {
                                // Set up the paths as needed for testing
                            }
                        },
                        new JsonMediaTypeFormatter())
                });

            var petService = new PetService(httpClientMock.Object);

            // Act
            var result = await petService.DisplayAvailablePets();

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions based on your specific expectations
        }
    }
}
