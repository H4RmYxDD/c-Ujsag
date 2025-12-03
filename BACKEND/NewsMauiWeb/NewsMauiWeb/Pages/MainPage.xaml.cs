using Common;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NewsApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5038/")
        };

        LoadTokenAsync();
    }

    private async void LoadTokenAsync()
    {
        var token = await SecureStorage.GetAsync("auth_token");

        if (!string.IsNullOrEmpty(token))
        {
            // token tisztítása
            token = token.Trim();
            token = token.Replace("\r", "").Replace("\n", "");

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadArticlesAsync();
    }

    private async Task LoadArticlesAsync()
    {
        try
        {
            var articles = await _httpClient.GetFromJsonAsync<List<ArticleDto>>("list");
            ArticleCollectionView.ItemsSource = articles;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", ex.Message, "OK");
        }
    }

    private async void OnAddArticlePageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddArticlePage(_httpClient));
    }

    private async void OnEditArticleClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is ArticleDto article)
        {
            await Navigation.PushAsync(new EditArticlePage(_httpClient, article.Id));
        }
    }

    private async void OnDeleteArticleClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is ArticleDto article)
        {
            bool confirm = await DisplayAlert(
                "Törlés megerõsítése",
                $"Biztosan törlöd a(z) '{article.Title}' cikket?",
                "Igen", "Mégse");

            if (!confirm)
                return;

            var response = await _httpClient.DeleteAsync($"delete/{article.Id}");

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("OK", "Törölve.", "OK");
                await LoadArticlesAsync();
            }
            else
            {
                await DisplayAlert("Hiba", "Nem sikerült törölni!", "OK");
            }
        }
    }
}
