using DevExtreme_SelectBox.DevExpress.ui;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.SelectBox.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...

            dxSelectBox.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/scripts/jquery.min.js";
            dxSelectBox.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/scripts/dx.all.js";
            dxSelectBox.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/styles/dx.common.css";
            dxSelectBox.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = await SelectBox.JSInstanceLoaded;
            if (result)
            {
                SelectBox.Visibility = Visibility.Visible;
            }
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            SelectBox = null;
        }
    }
}
