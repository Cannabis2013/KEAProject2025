using ALAdmin.DataTransferObjects.Members;
using ALAdmin.Models.Members;

namespace ALAdmin.Services.Members;

public class MembersFilter : IFilter<FilterValues, MemberListItem>
{
    private string Phrase { get; set; } = "";
    private FilterValues Values { get; set; } = new();
    private List<MemberListItem>? Members { get; set; } = new();

    private bool HasPaid(MemberListItem item)
    {
        if (Values is { HasPaid: true, HasNotPaid: true }) return true;
        var currentDate = DateTime.Now.Date;
        var nextPayment = item.LastPayment.AddYears(1);
        if (Values.HasPaid) return nextPayment >= currentDate;
        if (!Values.HasNotPaid) return false;
        return nextPayment <= currentDate;
    }

    private static string FullName(MemberListItem item) => $"{item.FirstName} {item.LastName}";

    public List<MemberListItem> Filtered()
    {
        return Members?.Where(HasPaid)
            .Where(item => FullName(item).Contains(Phrase))
            .ToList() ?? [];
    }

    public void UpdateValues(FilterValues args) => Values = args;
    public void UpdatePhrase(string phrase) => Phrase = phrase;
    public void SetList(List<MemberListItem> list) => Members = list;
}