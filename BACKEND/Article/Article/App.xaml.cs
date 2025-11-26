using ArticleMaui.Pages;

namespace Article.WinUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ArticleMaui.Pages.LoginPage), typeof(ArticleMaui.Pages.LoginPage));
            Routing.RegisterRoute(nameof(ArticleMaui.Pages.RegisterPage), typeof(ArticleMaui.Pages.RegisterPage));
            Routing.RegisterRoute(nameof(ArticleMaui.Pages.MainPage), typeof(ArticleMaui.Pages.MainPage));
            AppShell MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}