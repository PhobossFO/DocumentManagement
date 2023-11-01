using Microsoft.AspNetCore.Mvc;
using DocumentManagementAPI.DTOs;

namespace DocumentManagementAPI.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(
            IDocumentRepository documentRepository
            )
        {
            _documentRepository = documentRepository;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _documentRepository.GetDocumentAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);

        }

        [HttpPost()]
        public async Task<IActionResult> CreateDocument(DocumentModifyDTO doc)
        {
            var document = await _documentRepository.CreateDocumentAsync(doc);

            return Ok(document);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, DocumentModifyDTO doc)
        {
            var document = await _documentRepository.UpdateDocumentAsync(id, doc);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }
    }
}