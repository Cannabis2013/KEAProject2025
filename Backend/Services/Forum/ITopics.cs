using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopics
{
    public Task<List<Topic>> ManyAsync(int pageIndex,int pageSize, Member currentMember);
    public Task<Topic?> OneAsync(int topicId);
    public Task<List<Topic>> RecentlyActiveAsync(int count);
    public Task<int> AddAsync(TopicUpdateRequest request, Member currentMember);
    public Task<bool> UpdateAsync(TopicUpdateRequest request, Member currentMember);
    public Task<bool> RemoveAsync(int topicId, Member currentMember);
}