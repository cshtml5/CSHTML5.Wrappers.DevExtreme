using DevExtreme_Switch.DevExpress.ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.Switch.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxSwitch.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch.Examples/scripts/jquery.min.js";
            dxSwitch.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch.Examples/scripts/dx.all.js";
            dxSwitch.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch.Examples/styles/dx.common.css";
            dxSwitch.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch.Examples/styles/dx.light.css";

            this.Loaded += MainPage_Loaded;

        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //await Switch1.JSInstanceLoaded;
            //Switch1.onValueChanged((Action<object>)(e2 =>
            //{
            //    MessageBox.Show("Value has changed");
            //}));
        }

        void Button1_Click(object sender, RoutedEventArgs e)
        {
            Switch1.Value = !Switch1.Value;
        }

        void Button2_Click(object sender, RoutedEventArgs e)
        {
            var switchOptions = new dxSwitchOptions()
            {
                switchedOnText = "I am ON",
                switchedOffText = "I am OFF",
                onValueChanged = (Action<object>)(e2 =>
                    {
                        MessageBox.Show("Value has changed");
                    })
            };
            Switch1.option(switchOptions);
        }
    }
}
