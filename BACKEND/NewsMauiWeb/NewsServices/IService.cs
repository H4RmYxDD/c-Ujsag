using Common;
using NewsDataBase;

namespace NewsServices
{
    public interface IService
    {
        Task CreateArticleAsync(Article entity);
        Task<ArticleDto> GetArticleAsync(int id);
        Task<List<ArticleDto>> ListAllArticleAsync();
        Task DeleteArticleAsync(int id);
        Task UpdateArticleAsync(Article model);
    }
}
