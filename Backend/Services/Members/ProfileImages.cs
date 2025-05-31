using ALBackend.Entities.Members;
using ALBackend.Persistence;

namespace ALBackend.Services.Members;

public class ProfileImages(MariaDbContext dbContext) : IProfileImages
{
    public ProfileImage? One(int id) => dbContext.ProfileImages.Find(id);
    
    public ProfileImage? OneFromMemberId(int memberId) => dbContext.ProfileImages.FirstOrDefault(image => image.memberId == memberId);

    public List<ProfileImage> Many(int memberId)
    {
        return dbContext
            .ProfileImages
            .Where(image => image.memberId == memberId)
            .ToList();
    }
}