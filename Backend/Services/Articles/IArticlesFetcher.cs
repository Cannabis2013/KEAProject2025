using ALBackend.DataTransferObject.Articles;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Articles;

public interface IArticlesFetcher
{
    public List<ArticleCard> Fetch();
    public List<ArticleCard> Fetch(int count);
    public JsonResult FromUser(int userId);
    public JsonResult One(int id);
}