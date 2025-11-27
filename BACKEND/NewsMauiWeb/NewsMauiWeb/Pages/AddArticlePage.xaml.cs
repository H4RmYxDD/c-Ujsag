using NewsDataBase;
using NewsServices;

namespace NewsApp.Pages;

public partial class AddArticlePage : ContentPage
{
    private readonly IService _Service;

    public AddArticlePage(IService Service)
    {
        InitializeComponent();
        _Service = Service;
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
            Title = TitleEntry.Text,
            Content = ContentEntry.Text,
        };

        await _Service.CreateArticleAsync(article);

        await DisplayAlert("Siker", "Az újság hozzáadva!", "OK");
        await Navigation.PopAsync();
    }
}