﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.DataGrid.Examples
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
