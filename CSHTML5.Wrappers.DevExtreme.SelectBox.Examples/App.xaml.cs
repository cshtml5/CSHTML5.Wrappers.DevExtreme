using Windows.UI.Xaml;

namespace CSHTML5.Wrappers.DevExtreme.SelectBox.Examples
{
    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            // Enter construction logic here...

            var mainPage = new MainPage();
            Window.Current.Content = mainPage;
        }
    }
}
