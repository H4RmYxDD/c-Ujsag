namespace NewsApp.Pages;
using NewsServices;
using Common;
using System.Net.Http.Json;
using NewsDataBase;

public partial class MainPage : ContentPage
{
    private readonly IService _Service;
    private readonly HttpClient _httpClient;
    public MainPage(IService Service)
    {
        InitializeComponent();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5038/") // Backend címed
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadArticlesAsync();
    }

    private async Task LoadArticlesAsync()
    {

        var articles = await _httpClient.GetFromJsonAsync<List<ArticleDto>>("/list");
        ArticleCollectionView.ItemsSource = articles;
    }

    private async void OnAddArticlePageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddArticlePage(_Service));
    }

    private async void OnEditArticleClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is ArticleDto article)
        {
            await Navigation.PushAsync(new EditArticlePage(_Service, article.Id));
        }
    }

    private async void OnDeleteArticleClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is ArticleDto article)
        {
            bool confirm = await DisplayAlert(
                "Törlés megerõsítése",
                $"Biztosan törölni szeretnéd ezt az ujsagot? ({article.Title} {article.Content})",
                "Igen", "Mégse");

            if (confirm)
            {
                try
                {
                    await _Service.DeleteArticleAsync(article.Id);
                    await DisplayAlert("Siker", "Az ujsag törölve lett.", "OK");
                    await LoadArticlesAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Hiba", $"Nem sikerült törölni az ujsagot: {ex.Message}", "OK");
                }
            }
        }
    }
}
