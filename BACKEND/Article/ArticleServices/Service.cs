using Common;
using ArticleDataBase;
using Microsoft.EntityFrameworkCore;

namespace ArticleServices;

public class Service(ArticleDbContext db) : IService
{
    public async Task CreateArticleAsync(Article entity)
    {
        db.Articles.Add(entity);
        await db.SaveChangesAsync();
    }

    public async Task CreateAuthorAsync(Author entity)
    {
        db.Authors.Add(entity);
        await db.SaveChangesAsync();
    }

    public async Task<ArticleDto> GetArticleAsync(int id)
    {
        var entity = await db.Articles.FirstOrDefaultAsync(e => e.Id == id);

        return new ArticleDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
        };
    }

    public async Task<List<ArticleDto>> ListAllArticleAsync()
    {
        return await db.Articles.Select(e => new ArticleDto
        {
            Id = e.Id,
            Title= e.Title,
            Content = e.Content,
        }).ToListAsync();
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await db.Articles.FirstOrDefaultAsync(e => e.Id == id);
        if (article != null)
        {
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
        }
    }


    public async Task UpdateArticleAsync(Article model)
    {
        await db.Articles.Where(e => e.Id == model.Id).ExecuteUpdateAsync(
            setters =>
                setters.SetProperty(e => e.Title, model.Title)
                        .SetProperty(e => e.Content, model.Content));
    }
}