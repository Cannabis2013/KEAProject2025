using ALBackend.Entities.Members;

namespace ALBackend.Services.Members;

public interface IProfileImages
{
    public ProfileImage? One(int id);
    public ProfileImage? OneFromMemberId(int memberId);
    public List<ProfileImage> Many(int memberId);
}