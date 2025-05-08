using ALAdmin.DataTransferObjects.Users;

namespace ALAdmin.Models.Controls;

public class ListItem
{
    public static ListItem FromUser(UserListItem user)
    {
        return new()
        {
            Text = user.Email,
            Value = user.Id.ToString()
        };
    }
    
    public required string Text { get; init; }
    public required string Value { get; init; }
}