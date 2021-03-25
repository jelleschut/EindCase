using EindCase.Api.DTO;
using EindCase.Api.Services.Interfaces;
using EindCase.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EindCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IInputValidatorService _validatorService;
        private readonly IStringToCourseConvertService _convertService;
        private readonly ICourseInsertService _insertService;

        public FileUploadController(IStringToCourseConvertService convertService,
                                    ICourseInsertService insertService,
                                    IInputValidatorService inputValidatorService)
        {
            _convertService = convertService;
            _insertService = insertService;
            _validatorService = inputValidatorService;
        }

        // POST api/<FileUploadController>
        [HttpPost]
        public async Task<UploadResponse> Post()
        {
            UploadResponse response = new UploadResponse();

            if(!_validatorService.ValidateRequest(Request))
            {
                response.Error = true;
                response.ErrorMessage = "Bestand kan niet worden gevonden";
                return response;
            }

            var file = Request.Form.Files[0];

            if (!_validatorService.ValidateFileExtension(file))
            {
                response.Error = true;
                response.ErrorMessage = "Bestand is niet in correct formaat.";
                return response;
            }
            
            var fileContent = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    fileContent.AppendLine(reader.ReadLine());
            }

            int lineNumber = _validatorService.ValidateString(fileContent.ToString());

            if (lineNumber > 0)
            {
                response.Error = true;
                response.ErrorMessage = $"Bestand is niet in correct formaat op regel {lineNumber}.\nEr zijn geen cursusinstanties toegevoegd.";
                return response;
            }

            List<CourseInstance> courseInstances = _convertService.Convert(fileContent.ToString());

            (response.NewCourses, response.NewInstances, response.Duplicates) = await _insertService.InsertInstances(courseInstances);

            return response;
        }
    }
}
