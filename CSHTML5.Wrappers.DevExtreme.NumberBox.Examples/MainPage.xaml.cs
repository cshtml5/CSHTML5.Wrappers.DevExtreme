using DevExtreme_NumberBox.DevExpress.ui;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.NumberBox.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxNumberBox.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox.Examples/scripts/jquery.min.js";
            dxNumberBox.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox.Examples/scripts/dx.all.js";
            dxNumberBox.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox.Examples/styles/dx.common.css";
            dxNumberBox.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            //Enter constructor logic here...
        }

        private async void MainPage_Loaded(Object sender, RoutedEventArgs e)
        {
            await NumberBox1.JSInstanceLoaded;
            NumberBox1.Value = "777888";
            NumberBox1.Placeholder = "Please provide some lucky numbers";
            NumberBox1.ShowClearButton = true;
      
            await NumberBox2.JSInstanceLoaded;
            NumberBox2.Value = "0";
            NumberBox2.ShowSpinButtons = true;

            await NumberBox3.JSInstanceLoaded;
            NumberBox3.Format = "$ #,##0.00";

            await NumberBox4.JSInstanceLoaded;
            NumberBox4.Value = "0";

            await NumberBox4b.JSInstanceLoaded;
            NumberBox4b.Value = "0";

            await NumberBox4c.JSInstanceLoaded;
            NumberBox4c.Value = "0";
            NumberBox4c.ReadOnly = true;
        }

        private async void MainPage_Unloaded(Object sender, RoutedEventArgs e)
        {
            //Do something
        }

        void GetValue_Click(Object sender, RoutedEventArgs e)
        {
            Object currentValue = NumberBox1.CurrentNumberBoxValue;
            Object previousValue = NumberBox1.PreviousNumberBoxValue;

            if (currentValue == null)
            {
                MessageBox.Show("Please Enter A Value");
            }
            else
            {
                NumberBox1.Value = currentValue.ToString();
                MessageBox.Show(String.Format("Current Value: {0}, Previous Value: {1}", currentValue.ToString(), previousValue.ToString()));
            }
        }

        void AddValue_Click(Object sender, RoutedEventArgs e)
        {
            Object currentValue = NumberBox4.CurrentNumberBoxValue;
            Object currentValue2 = NumberBox4b.CurrentNumberBoxValue;

            if (currentValue != null || currentValue2 != null)
            {
                NumberBox4c.Value = Convert.ToString(Convert.ToDouble(currentValue.ToString()) + Convert.ToDouble(currentValue2.ToString()));
            }
        }
    }
}
