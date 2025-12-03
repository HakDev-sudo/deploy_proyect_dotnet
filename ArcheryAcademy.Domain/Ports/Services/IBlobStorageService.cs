namespace ArcheryAcademy.Domain.Ports.Services;

public interface IBlobStorageService
{
 
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);


    Task DeleteAsync(string fileName);


    string GetBlobUrl(string fileName);
}
