using DocumentManagementAPI.DTOs;
using DocumentManagementAPI.Entities;

namespace DocumentManagementAPI
{
    public interface IDocumentRepository
    {
        Task<Document> CreateDocumentAsync(DocumentModifyDTO document);

        Task<Document> GetDocumentAsync(int id);

        Task<Document> UpdateDocumentAsync(int documentId, DocumentModifyDTO document);
    }
}