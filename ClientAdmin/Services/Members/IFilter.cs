namespace ALAdmin.Services.Members;

public interface IFilter<in TArgs,TModel> where TModel : class
{
    public List<TModel> Filtered();
    public void UpdateValues(TArgs args);
    public void UpdatePhrase(string phrase);
    public void SetList(List<TModel> list);
}