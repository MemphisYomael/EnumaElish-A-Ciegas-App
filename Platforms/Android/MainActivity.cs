using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace EnumaElishApp
{
    [Activity(Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize
        | ConfigChanges.Orientation
        | ConfigChanges.UiMode
        | ConfigChanges.Density
        | ConfigChanges.ScreenLayout
        | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.SensorLandscape
        )]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.R) // Android 11+
            {
                var controller = Window.InsetsController;
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
                Window.SetDecorFitsSystemWindows(false);

                Window.DecorView.WindowInsetsController?.Hide(WindowInsets.Type.NavigationBars() | WindowInsets.Type.StatusBars());

                if (controller != null)
                {
                    controller.Hide(WindowInsets.Type.NavigationBars() | WindowInsets.Type.StatusBars());
                    controller.SystemBarsBehavior = (int)WindowInsetsControllerBehavior.ShowTransientBarsBySwipe;
                }
            }
            else // Android 10 o anterior
            {
#pragma warning disable CA1422 // Suprimimos obsolescencia para versiones anteriores
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                    SystemUiFlags.ImmersiveSticky |
                    SystemUiFlags.Fullscreen |
                    SystemUiFlags.HideNavigation |
                    SystemUiFlags.LayoutFullscreen |
                    SystemUiFlags.LayoutHideNavigation |
                    SystemUiFlags.LayoutStable);
#pragma warning restore CA1422
            }
        }
    }
    }
