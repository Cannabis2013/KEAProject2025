using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Members;

public interface IMembersFetch
{
    public Task<JsonResult> One(int memberId);
    public Task<JsonResult> One(Guid memberId);
    public JsonResult All();
}