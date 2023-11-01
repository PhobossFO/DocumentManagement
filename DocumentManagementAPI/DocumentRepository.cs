using DocumentManagementAPI.DTOs;
using DocumentManagementAPI.Entities;

namespace DocumentManagementAPI
{
    public class DocumentRepository : IDocumentRepository
    {

        private static List<Document> _documents = new List<Document>();

        private static int _idIterator = 0;

        public async Task<Document> CreateDocumentAsync(DocumentModifyDTO document)
        {
            var doc = new Document()
            {
                Id = ++_idIterator,
                Tags = document.Tags,
                Data = document.Data
            };

            _documents.Add(doc);
            return doc;
        }

        public async Task<Document> GetDocumentAsync(int id)
        {
            var document = _documents.FirstOrDefault(x => x.Id == id);
            return document;
        }

        public async Task<Document> UpdateDocumentAsync(int documentId,DocumentModifyDTO document)
        {
            var documentToUdpate = _documents.FirstOrDefault(x => x.Id == documentId);
            if (documentToUdpate != null)
            {
                documentToUdpate.Tags = document.Tags;
                documentToUdpate.Data = document.Data;

                return _documents.FirstOrDefault(x => x.Id == documentId)!;
            }

            else
            { 
                return null; 
            }
        }
    }
}
