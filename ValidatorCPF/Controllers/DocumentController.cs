using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using ValidatorCPF.Application.IApplication;

namespace ValidatorCPF.Controllers
{
    [ApiController] 
    public class DocumentController : ControllerBase
    {
        public readonly IDocument _documentService;

        public DocumentController(IDocument documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        [Route("api/document/validate")]
        public async Task<IActionResult> Validate([FromBody] DocumentDto cpfCnpj)
        {

            return await _documentService.ValidateDocument(cpfCnpj);
        }
    }
}
