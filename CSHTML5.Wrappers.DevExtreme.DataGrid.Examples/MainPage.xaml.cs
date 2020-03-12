﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CSHTML5.Wrappers.DevExtreme.DataGrid;
using CSHTML5.Wrappers.DevExtreme.Common;
using DevExtreme_DataGrid.DevExpress.ui;

using DevExtreme_DataGrid.DevExpress.data;
using AnonymousTypes;

namespace CSHTML5.Wrappers.DevExtreme.DataGrid.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxDataGrid.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid.Examples/scripts/jquery.min.js";
            dxDataGrid.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid.Examples/scripts/dx.all.js";
            dxDataGrid.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid.Examples/styles/dx.common.css";
            dxDataGrid.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid.Examples/styles/dx.light.css";

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            // Enter construction logic here...
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await DataGrid.JSInstanceLoaded; // Wait until the underlying JS instance of the DevExtreme DataGrid has been loaded

            SetColumnsData();

            SetDataSource();
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            await DataGrid.JSInstanceLoaded; // Wait until the underlying JS instance of the DevExtreme DataGrid has been loaded

            DataGrid.UnsubscribeFromDataSourceEvent();

        }

        private void SetDataSource()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            Employee firstEmployee = new Employee(1, "John", "Heart", new DateTime(1995, 01, 15));
            Employee secondEmployee = new Employee(2, "Olivia", "Peyton", new DateTime(2012, 05, 14));
            Employee thirdEmployee = new Employee(3, "Robert", "Reagan", new DateTime(2002, 11, 18));

            employees.Add(firstEmployee);
            employees.Add(secondEmployee);
            employees.Add(thirdEmployee);

            DataGrid.DataSource = employees;

            DataGrid2.ItemsSource = employees;
        }

        private void SetColumnsData()
        {
            List<dxDataGridColumn> columns = new List<dxDataGridColumn>();

            dxDataGridColumn idColumn = new dxDataGridColumn()
            {
                dataField = "ID",
                caption = "ID",
                dataType = "number",
                format = "",
                alignment = "left",
                allowGrouping = true
            };

			columns.Add(idColumn);

            dxDataGridColumn firstNameColumn = new dxDataGridColumn();
            firstNameColumn.dataField = "firstName";
            firstNameColumn.caption = "First Name";
            firstNameColumn.dataType = "string";
            firstNameColumn.format = "";
            firstNameColumn.alignment = "left";
            firstNameColumn.allowGrouping = true;
            columns.Add(firstNameColumn);

            dxDataGridColumn lastNameColumn = new dxDataGridColumn();
            lastNameColumn.dataField = "lastName";
            lastNameColumn.caption = "Last Name";
            lastNameColumn.dataType = "string";
            lastNameColumn.format = "";
            lastNameColumn.alignment = "left";
            lastNameColumn.allowGrouping = true;
            columns.Add(lastNameColumn);


            dxDataGridColumn hireDateColumn = new dxDataGridColumn();
            hireDateColumn.dataField = "hireDate";
            hireDateColumn.caption = "Hire Date";
            hireDateColumn.dataType = "date";
            hireDateColumn.format = "";
            hireDateColumn.alignment = "left";
            hireDateColumn.allowGrouping = true;
            columns.Add(hireDateColumn);


            DataGrid.Columns = columns;
        }
    }
}
