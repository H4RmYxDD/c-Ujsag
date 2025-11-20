using System.Net.Http.Json;

namespace ArticleMaui.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public RegisterPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5038/") // Backend címed
        };
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;

        StatusLabel.TextColor = Colors.Red;
        StatusLabel.Text = "";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            StatusLabel.Text = "Kérlek, tölts ki minden mezõt!";
            return;
        }

        if (password != confirmPassword)
        {
            StatusLabel.Text = "A jelszavak nem egyeznek!";
            return;
        }

        // MAUI-ban a JSON mezõk neve pontosan egyezzen a backend Identity API-val
        var registerRequest = new
        {
            authorName = email,   // IdentityUser UserName mezõ
            authorEmail = email,
            authorPassword = password
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/Account/register", registerRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.Headers.ToString();
                await SecureStorage.SetAsync("auth_token", token);

                StatusLabel.TextColor = Colors.Green;
                StatusLabel.Text = "Sikeres regisztráció!";

                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                // Backend hibájának kiíratása
                var errorMsg = await response.Content.ReadAsStringAsync();
                StatusLabel.Text = $"Hiba a regisztráció során: {errorMsg}";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Hiba történt a regisztráció közben: {ex.Message}";
            Console.WriteLine(ex);
        }
    }
}
