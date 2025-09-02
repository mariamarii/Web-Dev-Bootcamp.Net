using WebApplication2.Interfaces;

namespace WebApplication2.Services;
public class FileUpload : IFileUpload
{
    private readonly IWebHostEnvironment _env;

    public FileUpload(IWebHostEnvironment env) => _env = env;

    public async Task<string> UploadEmployeeImageAsync(IFormFile file)
    {
        if (file == null) return string.Empty;

        var folder = Path.Combine(_env.WebRootPath, "employee");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(folder, fileName);

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);

        }

        return $"/employee/{fileName}";
    }
}