using ALBackend.Entities.ImageGallary;

namespace ALBackend.Services.ImageGallary;

public interface IImages
{
    public Task<List<Image>> ManyAsync();
    public Task<List<Image>> ManyAsync(int count);
    public Task<List<Image>> ManyRandomAsync(int count);
}