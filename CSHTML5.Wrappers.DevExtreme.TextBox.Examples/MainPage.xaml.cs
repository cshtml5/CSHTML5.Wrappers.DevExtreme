using DevExtreme_TextBox.DevExpress.ui;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.TextBox.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...

            dxTextBox.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/scripts/jquery.min.js";
            dxTextBox.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/scripts/dx.all.js";
            dxTextBox.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/styles/dx.common.css";
            dxTextBox.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = await TextBox.JSInstanceLoaded;
            if (result)
            {
                TextBox.Visibility = Visibility.Visible;
            }
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            TextBox = null;
        }
    }
}
