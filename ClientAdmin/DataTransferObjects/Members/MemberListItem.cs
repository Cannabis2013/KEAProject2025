namespace ALAdmin.DataTransferObjects.Members;

public class MemberListItem
{
    public int MemberId { get; set; } = 666;
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? Email { get; set; } = "Loyalist@gmail.com";
    public DateTime LastPayment { get; set; }
}