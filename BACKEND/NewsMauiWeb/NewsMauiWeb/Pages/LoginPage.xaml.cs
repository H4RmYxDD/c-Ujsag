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
            BaseAddress = new Uri("http://localhost:5038/")
        };
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.Text = "Kérlek, tölts ki minden mezõt!";
            return;
        }

        var loginRequest = new { email = email, password = password };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("Account/Login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                StatusLabel.TextColor = Colors.Red;
                StatusLabel.Text = "Hibás email vagy jelszó!";
                return;
            }

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginResult == null || string.IsNullOrEmpty(loginResult.Token))
            {
                StatusLabel.TextColor = Colors.Red;
                StatusLabel.Text = "A token nem érkezett meg!";
                return;
            }

            await SecureStorage.SetAsync("auth_token", loginResult.Token);

            StatusLabel.TextColor = Colors.Green;
            StatusLabel.Text = "Sikeres bejelentkezés!";

            await Shell.Current.GoToAsync("//MainPage");
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

public class LoginResponse
{
    public string Token { get; set; }
}
