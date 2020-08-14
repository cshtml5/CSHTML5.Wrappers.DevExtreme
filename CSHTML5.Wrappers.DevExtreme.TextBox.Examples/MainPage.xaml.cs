using DevExtreme_TextBox.DevExpress.ui;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.TextBox.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxTextBox.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/scripts/jquery.min.js";
            dxTextBox.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/scripts/dx.all.js";
            dxTextBox.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/styles/dx.common.css";
            dxTextBox.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            //Enter constructor logic here...
        }

        private async void MainPage_Loaded(Object sender, RoutedEventArgs e)
        {
            await TextBox1.JSInstanceLoaded;
            TextBox1.Value = "FBC Default Data";
            TextBox1.Placeholder = "Please provide some default data";
            TextBox1.ShowClearButton = true;

            await TextBox2.JSInstanceLoaded;
            TextBox2.Mask = "+1 (000) 000-0000";
            TextBox2.MaskInvalidMessage = "A valid phone number is required";

            await TextBox3.JSInstanceLoaded;
            TextBox3.Mask = "000-000-000";
            TextBox3.MaskInvalidMessage = "Mr. Luhn would not approve this";

            await TextBox4.JSInstanceLoaded;
            TextBox4.Mask = "AAAaAAA";
            TextBox4.MaskInvalidMessage = "ZIP or Postal Code only";

            await TextBox5.JSInstanceLoaded;
            TextBox5.Mode = "Password";

            await TextBox6.JSInstanceLoaded;
            TextBox6.Value = GetViewModel(sender).TestProperty;

            await TextBox7.JSInstanceLoaded;
            TextBox7.Placeholder = "Type to change text in text box below";      

            await TextBox7b.JSInstanceLoaded;
        }

        private async void MainPage_Unloaded(Object sender, RoutedEventArgs e)
        {
            //Do something
        }

        void GetValue_Click(Object sender, RoutedEventArgs e)
        {
            Object currentValue = TextBox1.CurrentTextBoxValue;
            Object previousValue = TextBox1.PreviousTextBoxValue;

            if (currentValue == null)
            {
                MessageBox.Show("Please Enter A Value");
            }
            else
            {
                TextBox1.Value = currentValue.ToString();
                MessageBox.Show(String.Format("Current Value: {0}, Previous Value: {1}", currentValue.ToString(), previousValue.ToString()));
            }
        }

        private MyViewModel GetViewModel(Object sender)
        {
            return (MyViewModel)((FrameworkElement)sender).DataContext;
        }

        private void TextBox7_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Object currentValue = TextBox7.CurrentTextBoxValue;

            if (currentValue != null)
            {
                TextBox7b.Value = currentValue.ToString();
            }
        }

        private void TextBox8_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Object currentValue = TextBox8.CurrentTextBoxValue;

            if (currentValue != null)
            {
                TextBox8b.Value = currentValue.ToString();
            }
        }

        private void TextBox_KeyUpShared(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Object currentValue = TextBox7.CurrentTextBoxValue;
            Object currentValue2 = TextBox8.CurrentTextBoxValue;

            if (currentValue != null || currentValue2 != null)
            {
                TextBox7b.Value = currentValue.ToString();
                TextBox8b.Value = currentValue2.ToString();
            }
        }
    }
}
