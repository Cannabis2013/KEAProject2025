namespace ALBackend.DataTransferObject.Identity;

public class UserFetchResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public List<string> Roles { get; set; } = [];
}