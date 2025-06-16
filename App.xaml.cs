using EnumaElishApp.MVVM.Views.C_;

namespace EnumaElishApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new TabBar
            {
                Title = "Enuma Elish",
            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var mainPage = new NavigationPage(new HomeLoby())
            {
                BarBackgroundColor = Color.FromArgb("1f194a"),
                BarTextColor = Color.FromArgb("ffffff"),
                Title = "EnumaElishApp"
            };

            return new Window(mainPage);
        }
    }
}
