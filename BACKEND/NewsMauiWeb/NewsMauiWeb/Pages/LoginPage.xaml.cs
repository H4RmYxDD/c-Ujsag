using System.Net.Http.Json;

namespace NewsApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly HttpClient _httpClient;
    public LoginPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5038/") // Backend címed
        };
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            StatusLabel.Text = "Kérlek, tölts ki minden mez?t!";
            return;
        }

        var loginRequest = new { email = email, password = password };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("Account/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await SecureStorage.SetAsync("auth_token", token);

                StatusLabel.TextColor = Colors.Green;
                StatusLabel.Text = "Sikeres bejelentkezés!";

                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                StatusLabel.TextColor = Colors.Red;
                StatusLabel.Text = "Hibás email vagy jelszó!";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.Text = "Hiba történt a bejelentkezés közben.";
            Console.WriteLine(ex.Message);
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}
