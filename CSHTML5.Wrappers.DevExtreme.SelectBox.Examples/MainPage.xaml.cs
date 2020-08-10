using DevExtreme_SelectBox.DevExpress.ui;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Wrappers.DevExtreme.SelectBox.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxSelectBox.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/scripts/jquery.min.js";
            dxSelectBox.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/scripts/dx.all.js";
            dxSelectBox.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/styles/dx.common.css";
            dxSelectBox.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.SelectBox.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            //Enter constructor logic here...
        }

        private async void MainPage_Loaded(Object sender, RoutedEventArgs e)
        {
            await SelectBox1.JSInstanceLoaded;

            ObservableCollection<String> marvelCharacters = new ObservableCollection<String>();
            marvelCharacters.Add("Bruce Banner");
            marvelCharacters.Add("Peter Parker");
            marvelCharacters.Add("Jessica Jones");
            marvelCharacters.Add("Scott Summers");
            marvelCharacters.Add("Matt Murdock");
            marvelCharacters.Add("Bucky Barnes");
            marvelCharacters.Add("Wade Wilson");

            SelectBox1.DataSource = marvelCharacters;
            SelectBox1.SearchEnabled = true;

            await SelectBox2.JSInstanceLoaded;

            ObservableCollection<String> roles = new ObservableCollection<String>();
            roles.Add("Business Analyst");
            roles.Add("Project Manager");
            roles.Add("Quality Assurance Tester");
            roles.Add("Developer");
            SelectBox2.DataSource = roles;
            SelectBox2.ShowClearButton = true;
            SelectBox2.Placeholder = "Please provide some input here:";
        }

        private async void MainPage_Unloaded(Object sender, RoutedEventArgs e)
        {
            //Do something
        }

        void GetSelectedItem_Click(Object sender, RoutedEventArgs e)
        {
            Object selectedItem = SelectBox1.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Please Select An Item");
            }
            else
            {
                MessageBox.Show(selectedItem.ToString());
            }            
        }
    }
}
