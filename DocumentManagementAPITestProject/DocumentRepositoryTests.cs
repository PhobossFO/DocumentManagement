using DocumentManagementAPI.DTOs;
using NUnit.Framework;

namespace DocumentManagementAPI.Tests
{
    [TestFixture]
    public class DocumentRepositoryTests
    {

        [Test]
        public async Task CreateDocumentAsync_ShouldCreateDocument()
        {
            // Arrange
            var repository = new DocumentRepository();
            var documentDto = new DocumentModifyDTO
            {
                Tags = new string[] { "test" },
                Data = new Dictionary<string, string> { { "property", "value" } }
            };

            // Act
            var createdDocument = await repository.CreateDocumentAsync(documentDto);

            // Assert
            Assert.IsNotNull(createdDocument);
            Assert.That(new string[] { "test" }, Is.EqualTo(createdDocument.Tags));
            Assert.That(new Dictionary<string, string> { { "property", "value" } }, Is.EqualTo(createdDocument.Data));
        }

        [Test]
        public async Task GetDocumentAsync_ExistingDocument_ShouldReturnDocument()
        {
            // Arrange
            var repository = new DocumentRepository();
            var documentDto = new DocumentModifyDTO
            {
                Tags = new string[] { "test" },
                Data = new Dictionary<string, string> { { "property", "value" } }
            };
            var createdDocument = await repository.CreateDocumentAsync(documentDto);

            // Act
            var retrievedDocument = await repository.GetDocumentAsync(createdDocument.Id);

            // Assert
            Assert.IsNotNull(retrievedDocument);
            Assert.That(createdDocument.Id, Is.EqualTo(retrievedDocument.Id));
            Assert.That(createdDocument.Tags, Is.EqualTo(retrievedDocument.Tags));
            Assert.That(createdDocument.Data, Is.EqualTo(retrievedDocument.Data));
        }

        [Test]
        public async Task GetDocumentAsync_NonExistentDocument_ShouldReturnNull()
        {
            // Arrange
            var repository = new DocumentRepository();

            // Act
            var retrievedDocument = await repository.GetDocumentAsync(123);

            // Assert
            Assert.IsNull(retrievedDocument);
        }

        [Test]
        public async Task UpdateDocumentAsync_ExistingDocument_ShouldUpdateDocument()
        {
            // Arrange
            var repository = new DocumentRepository();
            var documentDto = new DocumentModifyDTO
            {
                Tags = new string[] { "test" },
                Data = new Dictionary<string, string> { { "property", "value" } }
            };
            var createdDocument = await repository.CreateDocumentAsync(documentDto);

            var updatedDto = new DocumentModifyDTO
            {
                Tags = new string[] { "testUpdated" },
                Data = new Dictionary<string, string> { { "propertyUpdated", "valueUpdated" } }
            };

            // Act
            var updatedDocument = await repository.UpdateDocumentAsync(createdDocument.Id, updatedDto);

            // Assert
            Assert.IsNotNull(updatedDocument);
            Assert.That(updatedDocument.Tags, Is.EqualTo(updatedDto.Tags));
            Assert.That(updatedDocument.Data, Is.EqualTo(updatedDto.Data));
        }

        [Test]
        public async Task UpdateDocumentAsync_NonExistentDocument_ShouldReturnNull()
        {
            // Arrange
            var repository = new DocumentRepository();
            var updatedDto = new DocumentModifyDTO
            {
                Tags = new string[] { "testUpdated" },
                Data = new Dictionary<string, string> { { "propertyUpdated", "valueUpdated" } }
            };

            // Act
            var updatedDocument = await repository.UpdateDocumentAsync(123, updatedDto);

            // Assert
            Assert.IsNull(updatedDocument);
        }

    }
}
