using ArcheryAcademy.Domain.Ports.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace ArcheryAcademy.Infrastructure.Adapters.Services;

public class AzureBlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _containerClient;
    private readonly string _containerName;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureStorage:ConnectionString"]
            ?? throw new InvalidOperationException("AzureStorage:ConnectionString no está configurado");
        
        _containerName = configuration["AzureStorage:ContainerName"] 
            ?? throw new InvalidOperationException("AzureStorage:ContainerName no está configurado");

        var blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        
        // Crear contenedor si no existe
        _containerClient.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        // Generar nombre único para evitar colisiones
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        
        var blobClient = _containerClient.GetBlobClient(uniqueFileName);

        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = contentType
        };

        await blobClient.UploadAsync(fileStream, new BlobUploadOptions
        {
            HttpHeaders = blobHttpHeaders
        });

        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string fileName)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }

    public string GetBlobUrl(string fileName)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);
        return blobClient.Uri.ToString();
    }
}
