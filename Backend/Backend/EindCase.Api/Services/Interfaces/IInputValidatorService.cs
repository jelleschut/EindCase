using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EindCase.Api.Services.Interfaces
{
    public interface IInputValidatorService
    {
        bool ValidateRequest(HttpRequest request);
        bool ValidateFileExtension(IFormFile file);
        int ValidateString(string input);
        bool TitleCheck(string line);
        bool CodeCheck(string line);
        bool LengthCheck(string line);
        bool DateCheck(string line);
    }
}
