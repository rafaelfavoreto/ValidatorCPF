using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using ValidatorCPF.Application.IApplication;

namespace ValidatorCPF.Application
{
    public class DocumentApplication : IDocument
    {
        public string Format(string document)
        {
            var digits = new string(document.Where(char.IsDigit).ToArray());

            if (digits.Length == 11)
                return Convert.ToUInt64(digits).ToString(@"000\.000\.000\-00");

            if (digits.Length == 14)
                return Convert.ToUInt64(digits).ToString(@"00\.000\.000\/0000\-00");

            return document; // Retorna como veio se não for válido
        }

        public static bool IsFormatted(string documento)
        {
            return Regex.IsMatch(documento, @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$") ||
                   Regex.IsMatch(documento, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$");
        }

        public Task<IActionResult> ValidateDocument(DocumentDto cpfCnpj)
        {
            if (cpfCnpj.Document == null || string.IsNullOrWhiteSpace(cpfCnpj.Document))
                return Task.FromResult<IActionResult>(new BadRequestObjectResult("Document cannot be null or empty"));

            bool isValid = !string.IsNullOrEmpty(cpfCnpj.Document);
            isValid = IsValid(cpfCnpj.Document);

            return Task.FromResult<IActionResult>(new OkObjectResult(new { IsValid = isValid, DocFormated = Format(cpfCnpj.Document) }));

        }

        public bool IsValid(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            tempCpf += resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(resto.ToString());
        }    
    }
}
