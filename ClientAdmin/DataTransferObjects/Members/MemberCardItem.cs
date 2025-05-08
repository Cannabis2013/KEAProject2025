namespace ALAdmin.DataTransferObjects.Members;

public class MemberCardItem
{
    public int Id { get; set; } = 666;
    public string? Name { get; set; } = "Navn";
    public string? Email { get; set; } = "Loyalist@gmail.com";
    public DateTime JoinedDate { get; set; } = DateTime.Today;
}