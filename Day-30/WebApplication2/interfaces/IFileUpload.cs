
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Interfaces;
public interface IFileUpload
{
    Task<string> UploadEmployeeImageAsync(IFormFile file);
}