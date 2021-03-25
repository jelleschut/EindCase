using EindCase.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EindCase.Api.Services
{
    public class InputValidatorService : IInputValidatorService
    {
        private const int CHUNKLENGTH = 5;

        public bool ValidateRequest(HttpRequest request)
        {
            return request.Form.Files.Count > 0;
        }

        public bool ValidateFileExtension(IFormFile file)
        {
            return file.FileName.EndsWith(".txt");
        }

        public int ValidateString(string input)
        {
            int chunkNumber = 0;

            string[] chunks = input.Trim().Split("\r\n\r\n");

            foreach (string chunk in chunks)
            {
                string[] lines = chunk.Split("\r\n");
                int lineNumber = 1;

                if (lines.Length < CHUNKLENGTH - 1)
                {
                    return (CHUNKLENGTH * chunkNumber) + lines.Length;
                }

                if (lines.Length > CHUNKLENGTH - 1)
                {
                    return CHUNKLENGTH * (chunkNumber + 1);
                }

                if (!TitleCheck(lines[0]))
                {
                    return lineNumber + (CHUNKLENGTH * chunkNumber);
                }

                lineNumber++;

                if (!CodeCheck(lines[1]))
                {
                    return lineNumber + (CHUNKLENGTH * chunkNumber);
                }
                lineNumber++;

                if (!LengthCheck(lines[2]))
                {
                    return lineNumber + (CHUNKLENGTH * chunkNumber);
                }
                lineNumber++;

                if (!DateCheck(lines[3]))
                {
                    return lineNumber + (CHUNKLENGTH * chunkNumber);
                }

                chunkNumber++;
            }
            return 0;
        }

        public bool TitleCheck(string line)
        {
            Regex r = new Regex(@"^Titel: .{1,300}$");
            return r.IsMatch(line);
        }

        public bool CodeCheck(string line)
        {
            Regex r = new Regex(@"^Cursuscode: \w{1,10}$");
            return r.IsMatch(line);
        }

        public bool LengthCheck(string line)
        {
            Regex r = new Regex(@"^Duur: \d dagen$");
            return r.IsMatch(line);
        }

        public bool DateCheck(string line)
        {
            Regex r = new Regex(@"^Startdatum: \d{1,2}/\d{1,2}/\d{4}$");
            return r.IsMatch(line);
        }
    }
}
