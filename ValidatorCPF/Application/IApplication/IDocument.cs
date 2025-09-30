using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ValidatorCPF.Application.IApplication
{
    public interface IDocument
    {
        Task<IActionResult> ValidateDocument(DocumentDto cpfCnpj);
        string Format(string document);
    }
}
