using Common;
using NewsDataBase;
using System.Net.Http.Json;

namespace NewsApp.Pages;

public partial class EditArticlePage : ContentPage
{
    private readonly HttpClient _httpClient;
    private ArticleDto _article;
    private readonly int _articleId;

    public EditArticlePage(HttpClient httpClient, int articleId)
    {
        InitializeComponent();
        _httpClient = httpClient;
        _articleId = articleId;

        LoadArticleAsync();
    }

    private async void LoadArticleAsync()
    {
        try
        {
            _article = await _httpClient.GetFromJsonAsync<ArticleDto>($"get/{_articleId}");

            if (_article == null)
            {
                await DisplayAlert("Hiba", "Nem található az újság!", "OK");
                await Navigation.PopAsync();
                return;
            }

            TitleEntry.Text = _article.Title;
            ContentEntry.Text = _article.Content;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", $"Hiba történt: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_article == null) return;

        var updatedArticle = new Article
        {
            Id = _article.Id,
            Title = TitleEntry.Text?.Trim(),
            Content = ContentEntry.Text?.Trim()
        };

        try
        {
            var response = await _httpClient.PutAsJsonAsync("update", updatedArticle);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Siker", "Az újság adatai frissítve lettek!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Hiba", "Nem sikerült frissíteni az újságot!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", $"Hiba történt: {ex.Message}", "OK");
        }
    }
}
