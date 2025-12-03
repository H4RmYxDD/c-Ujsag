using NewsDataBase;
using System.Net.Http.Json;

namespace NewsApp.Pages;

public partial class AddArticlePage : ContentPage
{
    private readonly HttpClient _httpClient;

    public AddArticlePage(HttpClient httpClient)
    {
        InitializeComponent();
        _httpClient = httpClient;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(ContentEntry.Text))
        {
            await DisplayAlert("Hiba", "A cím és tartalom mezõk kitöltése kötelezõ!", "OK");
            return;
        }

        var article = new Article
        {
            Title = TitleEntry.Text.Trim(),
            Content = ContentEntry.Text.Trim()
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("createArticle", article);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Siker", "Az újság hozzáadva!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Hiba", "Nem sikerült hozzáadni az újságot!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hiba", $"Hiba történt: {ex.Message}", "OK");
        }
    }
}
