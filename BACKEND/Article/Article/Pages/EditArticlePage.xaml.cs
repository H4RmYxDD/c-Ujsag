using ArticleDataBase;
using ArticleServices;
using Common;

namespace ArticleMaui.Pages;

public partial class EditArticlePage : ContentPage
{
    private readonly IService _Service;
    private ArticleDto _article;

    public EditArticlePage(IService carService, int carId)
    {
        InitializeComponent();
        _Service = carService;
        LoadArticleAsync(carId);
    }

    private async void LoadArticleAsync(int id)
    {
        _article = await _Service.GetArticleAsync(id);
        BindingContext = _article;

        TitleEntry.Text = _article.Title;
        ContentEntry.Text = _article.Content;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var updatedCar = new ArticleModel
        {
            Id = _article.Id,
            Title = TitleEntry.Text?.Trim(),
            Content = ContentEntry.Text?.Trim()
        };

        await _Service.UpdateArticleAsync(updatedCar);
        await DisplayAlert("? Siker", "Az ujsag adatai frissítve!", "OK");
        await Navigation.PopAsync();
    }
}