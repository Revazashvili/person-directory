namespace PersonDirectoryApi.Services;

public record Multimedia(string MimeType, byte[] Content);

public interface IMultimediaService
{
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    Task<Multimedia> GetAsync(string fileName, CancellationToken cancellationToken);
    Task RemoveAsync(string imageUrl);
    Task RemoveByNameAsync(string fileName);
}

public class MultimediaService : IMultimediaService
{
    private const string FolderPath = "media";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MultimediaService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        
        await using (var fileStream = new FileStream(GetFilePath(fileName), FileMode.Create))
        {
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        return GetImageUrl(fileName);
    }

    public async Task<Multimedia> GetAsync(string fileName, CancellationToken cancellationToken)
    {
        var filePath = GetFilePath(fileName);
        
        var mimeType = $"image/{Path.GetExtension(fileName).TrimStart('.')}";
        
        var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
        
        return new Multimedia(mimeType, bytes);
    }

    public async Task RemoveAsync(string imageUrl)
    {
        var fileName = imageUrl.Split('/').Last();
        
        var filePath = GetFilePath(fileName);
        
        File.Delete(filePath);
    }
    
    public async Task RemoveByNameAsync(string fileName)
    {
        var filePath = GetFilePath(fileName);
        
        File.Delete(filePath);
    }

    private string GetImageUrl(string imageName)
    {
        var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        return $"{baseUrl}/api/multimedia/{imageName}";
    }
    
    private string GetFilePath(string imageName)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        var filePath = Path.Combine(FolderPath, $"{imageName}");
        return filePath;
    }
}