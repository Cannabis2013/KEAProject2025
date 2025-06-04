using ALBackend.Entities.ImageGallary;
using ALBackend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.ImageGallary;

public class Images(MariaDbContext dbContext) : IImages
{
    public async Task<List<Image>> ManyAsync()
    {
        return await dbContext.Images.ToListAsync();
    }

    public async Task<List<Image>> ManyAsync(int count)
    {
        var length = await dbContext.Images.CountAsync();
        if (length <= 0 || count < length) return [];
        return await dbContext.Images.Take(count).ToListAsync();
    }

    public async Task<List<Image>> ManyRandomAsync(int count)
    {
        var length = await dbContext.Images.CountAsync();
        if (length <= 0 || count < length) return [];
        var images = dbContext.Images.ToList();
        var indexesAsArray = Enumerable.Range(0, length)
            .ToArray();
        var random = new Random();
        random.Shuffle(indexesAsArray);
        return indexesAsArray.ToList()
            .Take(count)
            .Select(index => images[index])
            .ToList();
    }
}