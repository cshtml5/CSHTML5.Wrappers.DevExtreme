using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.DataGrid;
using DevExtreme_DataGrid.DevExpress.data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

#if SLMIGRATION
using System.Windows;
#else
using Windows.UI.Xaml;
#endif
using ToJavaScriptObjectExtender;

namespace DevExtreme_DataGrid.DevExpress.ui
{
    public partial class dxDataGrid : JSComponent
    {
        const string ITEMS_ID_NAME_IN_JSON = "DevExtremeId";

        // used when the grid is set through PlainDataSource.
        Dictionary<int, object> _idsToItemsDictionary = new Dictionary<int, object>(); // Note: this dictionary serves to find the item from a given Id. It lets us find the item despite any change in order due to sorts, groupd and such.
        Dictionary<object, int> _itemsToIdsDictionary = new Dictionary<object, int>(); // Note: this dictionary serves to find the id associated to a given item from a given Id. It is useful for the actions that come from the thing that uses the Grid (for example to find a row associated to an item).

        List<Dictionary<string, object>> _plainDataSource;

        IEnumerable _dataSource;
        public IEnumerable DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                if (_dataSource != null)
                    UnsubscribeFromDataSourceEvent();

                _dataSource = value;

                RefreshGridDataSource();

                SubscribeToDataSourceEvent();
            }
        }

        private SearchPanelData _searchPanel;
        public SearchPanelData SearchPanel
        {
            get
            {
                return _searchPanel;
            }
            set
            {
                _searchPanel = value;
                SetSearchPanelDataOption(_searchPanel);
            }
        }

        private GroupPanelData _groupPanelData;
        public GroupPanelData GroupPanelData
        {
            get
            {
                return _groupPanelData;
            }
            set
            {
                _groupPanelData = value;
                SetGroupPanelDataOption(_groupPanelData);
            }
        }

        private dxDataGridSelection _dataGridSelection;
        public dxDataGridSelection DataGridSelection
        {
            get
            {
                return _dataGridSelection;
            }
            set
            {
                _dataGridSelection = value;
                SetDataGridSelectionDataOption(_dataGridSelection);
            }
        }

        private dxDataGridEditing _dataGridEditing;
        public dxDataGridEditing DataGridEditing
        {
            get
            {
                return _dataGridEditing;
            }
            set
            {
                _dataGridEditing = value;
                SetDataGridEditingOption(_dataGridEditing);
            }
        }

        private bool _allowColumnReordering;
        public bool AllowColumnReordering
        {
            get
            {
                return _allowColumnReordering;
            }
            set
            {
                _allowColumnReordering = value;
                option("allowColumnReordering", Utils.ToJSObject(_allowColumnReordering));
            }
        }

        private bool _rowAlternationEnabled;
        public bool RowAlternationEnabled
        {
            get
            {
                return _rowAlternationEnabled;
            }
            set
            {
                _rowAlternationEnabled = value;
                option("rowAlternationEnabled", Utils.ToJSObject(_rowAlternationEnabled));
            }
        }

        private bool _showBorders;
        public bool ShowBorders
        {
            get
            {
                return _showBorders;
            }
            set
            {
                _showBorders = value;
                option("showBorders", Utils.ToJSObject(_showBorders));
            }
        }

        public object SelectedItem
        {
            get
            {
                int itemId = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.getSelectedRowsData()[0] ? $0.getSelectedRowsData()[0][$1] : -1", UnderlyingJSInstance, ITEMS_ID_NAME_IN_JSON));
                if (itemId >= 0)
                    return _idsToItemsDictionary[itemId];
                return null;
            }
        }
        public List<object> SelectedItems
        {
            get
            {
                List<object> selectedItems = new List<object>();
                var itemsList = Interop.ExecuteJavaScript(@"$0.getSelectedRowsData()", UnderlyingJSInstance);
                int itemListLength = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.length", itemsList));
                for (int i = 0; i < itemListLength; i++)
                {
                    int itemId = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0[$1][$2]", itemsList, i, ITEMS_ID_NAME_IN_JSON));
                    selectedItems.Add(_idsToItemsDictionary[itemId]);
                }
                return selectedItems;
            }
        }

        #region selection changed event
        //note about this event: we need to register to this event, pre-handle it and only then change the value in c# since the user will probably want the old value if he registers to it.

        /// <summary>
        /// Occurs when the selection is changed.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Raises the TextChanged event
        /// </summary>
        /// <param name="eventArgs">The arguments for the event.</param>
        protected virtual void OnSelectionChanged(SelectionChangedEventArgs eventArgs)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, eventArgs);
            }
        }

        private void RegisterToSelectionChanged()
        {
            Interop.ExecuteJavaScript("$0.option($1, $2)", this.UnderlyingJSInstance, "onSelectionChanged", (Action<object>)HandleSelectionChanged);
        }

        //private void UnregisterFromSelectionChanged()
        //{
        //	Interop.ExecuteJavaScript("$0.option($1, undefined)", this.UnderlyingJSInstance, "onSelectionChanged");
        //}

        private void HandleSelectionChanged(object selectionChangedJSArgs)
        {
            if (SelectionChanged != null)
            {
                //Note: below, Key means the value of the key property of the data item (the key property is defined through keyExpr I think).
                //		(From the Doc: If a field providing key values is not specified in the data source, the whole data object is considered the key. In this case, all arrays passed to the function contain data objects instead of keys.)
                //		see: https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxDataGrid/Configuration/#onSelectionChanged

                //An array of the selected keys that was defined I don't know how (probs a property in the DataGrid that says "this property is the key in the data items")
                ////create the eventArgs from the JSArgs
                //SelectedItemsAsJSObject = Interop.ExecuteJavaScript("$0.selectedRowsData", selectionChangedJSArgs);
                ////currentDeselectedRowKeys --> contains the keys of the newly unselected items
                ////currentSelectedRowKeys --> contains the keys of the newly selected items
                ////selectedRowKeys --> contains the keys of all the currently selected items
                ////selectedRowsData --> An array of all the currently selected items


                //todo: FIX: Use currentDeselectedRowKeys for the removedItems and currentSelectedRowKeys for the addedItems. See the SelectedItems property implementation to change them into C# objects
                OnSelectionChanged(new SelectionChangedEventArgs(null, SelectedItems));
            }
        }

        #endregion


        public static Configuration Configuration = new Configuration();

        static JSLibrary _jsLibrary;

        public override JSLibrary JSLibrary { get { return _jsLibrary; } }

        // Used to avoid confilct with other jQueryVersion
        static object _jQueryVersion;

        partial void Initialize()
        {
            base.Initialize(initJSInstance: true);
        }
        protected override void InitializeJSInstance()
        {
            if (_jQueryVersion == null)
            {
                _jQueryVersion = Interop.ExecuteJavaScript(@"$.noConflict()");
            }

            Interop.ExecuteJavaScript(@"
var gridContainer = $0;
gridContainer.id = 'gridContainer';
", (new JSObject(this.DomElement)).ToJavaScriptObject());

            Interop.ExecuteJavaScript(@"$0('#gridContainer').dxDataGrid({});", _jQueryVersion);

            UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#gridContainer').dxDataGrid('instance')", _jQueryVersion);
            SetOnEditorPreparedOption();
            SetOnRowRemovedOption();
            SetOnRowInsertedOption();

            InitialiseDataGridEditing();
            InitialiseDataGridSelection();
            InitialiseGroupPanel();
            InitialiseSearchPanel();

            RegisterToSelectionChanged(); //todo-perf: only register when the developper registers to the SelectionChanged event.

            RowAlternationEnabled = true;
            ShowBorders = true;
            AllowColumnReordering = true;

            //note about the comment below: only one occurence of an id should be found in a html document and if there are multiple occurences, only the first can be accessed through its id so we can't use it. (see https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/id)
            //we remove the id since we won't use it anymore and it will let us reuse this id for another dxtextbox:
            Interop.ExecuteJavaScript(@"
var gridContainer = $0;
gridContainer.id = '';
", (new JSObject(this.DomElement)).ToJavaScriptObject());
        }

        protected override void JSComponent_Loaded(object sender, RoutedEventArgs e)
        {
            if (Configuration.AreSourcesSet)
            {
                if (_jsLibrary == null)
                {
                    _jsLibrary = new JSLibrary(
                        css: new Interop.ResourceFile[]
                        {
                            new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/styles/dx.common.css"
                            new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/styles/dx."theme name".css"
                        },
                        js: new Interop.ResourceFile[]
                        {
                            new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/jquery.min.js"
                            new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/dx.all.js"
                        }
                    );
                }
                base.JSComponent_Loaded(sender, e);
                CheckErrorandDisplayItInsteadOfEditorIfNeeded();
            }
            else
            {
                this.Html = @"Before you can use the DevExtreme DataGrid, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com";
                MessageBox.Show(@"Before you can use the DevExtreme DataGrid, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com"); //todo: put the address of the tutorial.
                base.AbortLoading();
            }
        }

        async void CheckErrorandDisplayItInsteadOfEditorIfNeeded()
        {
            if (!await this.JSInstanceLoaded)
            {
                this.Html = @"The libraries for the DevExtreme DataGrid could not be found. Make sure you have added them in your project at the location you specified in the Configuration.";
                MessageBox.Show(@"The libraries for the DevExtreme DataGrid could not be found. Make sure you have added them in your project at the location you specified in the Configuration.");
            }
        }

        private List<string> PropertyNames { get; set; }

        private IEnumerable<dxDataGridColumn> _columns;
        /// <summary>
        /// The columns to display in the Grid.
        /// </summary>
        public IEnumerable<dxDataGridColumn> Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
                UpdatePropertyNamesOnColumnsChange(value);
                option("columns", Utils.ToJSObject(_columns));
            }
        }

        public void SubscribeToDataSourceEvent()
        {
            if (_dataSource != null)
            {
                Type dataSourceType = _dataSource.GetType().GetGenericArguments()[0];
                if (typeof(INotifyPropertyChanged).IsAssignableFrom(dataSourceType))
                {
                    foreach (INotifyPropertyChanged data in _dataSource)
                    {
                        data.PropertyChanged += UpdateJSDataSource;
                    }
                }

                if (_dataSource is INotifyCollectionChanged)
                {
                    INotifyCollectionChanged dataSourceAsINotifyCollectionChanged = (INotifyCollectionChanged)_dataSource;
                    dataSourceAsINotifyCollectionChanged.CollectionChanged += OnCollectionChanged;
                }
            }
        }

        public void UnsubscribeFromDataSourceEvent()
        {
            if (_dataSource != null)
            {
                Type dataSourceType = _dataSource.GetType().GetGenericArguments()[0];
                if (typeof(INotifyPropertyChanged).IsAssignableFrom(dataSourceType))
                {
                    foreach (INotifyPropertyChanged data in _dataSource)
                    {
                        data.PropertyChanged -= UpdateJSDataSource;
                    }
                }

                if (_dataSource is INotifyCollectionChanged)
                {
                    INotifyCollectionChanged dataSourceAsINotifyCollectionChanged = (INotifyCollectionChanged)_dataSource;
                    dataSourceAsINotifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
                }
            }
        }

        public void AddObjectToDataSource(object objectToAdd)
        {
            if (_dataSource is IList)
            {
                Type objectToAddType = objectToAdd.GetType();
                Type dataSourceType = _dataSource.GetType().GetGenericArguments()[0];

                if (objectToAddType == dataSourceType)
                {
                    IList dataSource = (IList)_dataSource;
                    dataSource.Add(objectToAdd);

                    RefreshGridDataSource();
                }
            }
        }

        private void UpdatePropertyNamesOnColumnsChange(IEnumerable<dxDataGridColumn> columns)
        {
            if (columns != null)
            {
                if (PropertyNames != null)
                {
                    PropertyNames.Clear();
                }
                else
                {
                    PropertyNames = new List<string>();
                }
                foreach (dxDataGridColumn column in columns)
                {
                    PropertyNames.Add(column.dataField);
                }
            }
        }

        private void SetPropertyNamesIfColumnsNotAlredySet()
        {
            if (_dataSource != null)
            {
                Type dataSourceGenericType = _dataSource.GetType().GetGenericArguments()[0];

                if (PropertyNames != null)
                {
                    PropertyNames.Clear();
                }
                else
                {
                    PropertyNames = new List<string>();
                }

                foreach (PropertyInfo property in dataSourceGenericType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    PropertyNames.Add(property.Name);
                }
            }
        }

        public void SetSearchPanelDataOption(SearchPanelData searchPanelData)
        {
            option("searchPanel", Utils.ToJSObject(searchPanelData));
        }

        public void SetGroupPanelDataOption(GroupPanelData groupPanelData)
        {
            option("groupPanel", Utils.ToJSObject(groupPanelData));
        }

        public void SetDataGridSelectionDataOption(dxDataGridSelection dataGridSelection)
        {
            option("selection", Utils.ToJSObject(dataGridSelection));
        }

        public void SetDataGridEditingOption(dxDataGridEditing gridBaseEditing)
        {
            option("editing", Utils.ToJSObject(gridBaseEditing));
        }

        private void SetOnEditorPreparedOption()
        {
            Action<int, string, object> updateSource = (Action<int, string, object>)this.UpdateSource;

            Interop.ExecuteJavaScript(@"$0.option('onEditorPrepared', function (options) {
                if (options.parentType == 'dataRow' && options.type != 'selection')
                {
                    var selectedCell = options.editorElement[options.editorName]('instance');

                    selectedCell.option('onValueChanged', function (args) {
                        options.setValue(args.value);
                        if(options.row.data.DevExtremeId)
                            $1(options.row.data.DevExtremeId, options.dataField, args.value);
                    });
                };
            });", UnderlyingJSInstance, updateSource);
        }

        private void UpdateSource(int objectId, string propertyName, object newValue)
        {
            Type type = _dataSource.GetType().GetGenericArguments()[0];
            PropertyInfo property = type.GetProperty(propertyName);

            UpdateProperty(_idsToItemsDictionary[objectId], property, newValue);
        }

        private void UpdateJSDataSource(object sender, PropertyChangedEventArgs e)
        {
            int objectID = _itemsToIdsDictionary[sender];

            Type objectType = sender.GetType();
            PropertyInfo property = objectType.GetProperty(e.PropertyName);

            _plainDataSource[objectID][e.PropertyName] = property.GetValue(sender);

            var JSObject = Utils.ConvertPlainDataSourceToJson(_plainDataSource, PropertyNames, ITEMS_ID_NAME_IN_JSON);

            DataSource dataSource = new DataSource(JSObject);

            option("dataSource", dataSource);
        }

        private void SetOnRowRemovedOption()
        {
            Action<int> rowRemoved = (Action<int>)this.OnRowRemoved;
            Interop.ExecuteJavaScript(@"$0.option('onRowRemoved', function (options) {
                $1(options.data.DevExtremeId);
            });", UnderlyingJSInstance, rowRemoved);
        }

        private void OnRowRemoved(int objectId)
        {
            var objectToRemove = _idsToItemsDictionary[objectId];

            if (typeof(INotifyPropertyChanged).IsAssignableFrom(objectToRemove.GetType()))
            {
                INotifyPropertyChanged objectToRemoveAsINotifyPropertyChanged = (INotifyPropertyChanged)objectToRemove;
                objectToRemoveAsINotifyPropertyChanged.PropertyChanged -= UpdateJSDataSource;
            }


            Type dataSourceType = _dataSource.GetType();
            if (typeof(IList).IsAssignableFrom(dataSourceType))
            {
                IList dataSource = (IList)_dataSource;
                dataSource.Remove(objectToRemove);

                RefreshGridDataSource();
            }
        }
        private void RefreshGridDataSource()
        {
            if (PropertyNames == null)
                SetPropertyNamesIfColumnsNotAlredySet();

            _plainDataSource = Utils.ConvertEnumerableToPlainDataSource(_dataSource, PropertyNames, out _idsToItemsDictionary, out _itemsToIdsDictionary, ITEMS_ID_NAME_IN_JSON); //note: We force the presence of even the properties that have a null value in the json because otherwise it causes an error when kendo ui tries to get the value (since the property itself doesn't exist)

            var JSObject = Utils.ConvertPlainDataSourceToJson(_plainDataSource, PropertyNames, ITEMS_ID_NAME_IN_JSON);

            DataSource dataSource = new DataSource(JSObject);

            option("dataSource", dataSource);

            columnOption(ITEMS_ID_NAME_IN_JSON, "visible", Utils.ToJSObject(false));
        }

        private void SetOnRowInsertedOption()
        {
            Action<object> rowAdded = (Action<object>)this.OnRowAdded;
            Interop.ExecuteJavaScript(@"$0.option('onRowInserted', function (options) {
                $1(options.data);
            });", UnderlyingJSInstance, rowAdded);
        }

        private void OnRowAdded(object objectData)
        {
            Type dataSourceType = _dataSource.GetType();

            if (typeof(IList).IsAssignableFrom(dataSourceType))
            {
                Type dataSourceGenericType = _dataSource.GetType().GetGenericArguments()[0];
                IList dataSource = (IList)_dataSource;

                var newObject = Activator.CreateInstance(dataSourceGenericType);


                foreach (PropertyInfo property in dataSourceGenericType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    UpdateProperty(newObject, property, Interop.ExecuteJavaScript(@"$0[$1] ? $0[$1] : ''", objectData, property.Name));
                }

                dataSource.Add(newObject);

                RefreshGridDataSource();

                if (typeof(INotifyPropertyChanged).IsAssignableFrom(newObject.GetType()))
                {
                    INotifyPropertyChanged newObjectAsINotifyPropertyChanged = (INotifyPropertyChanged)newObject;
                    newObjectAsINotifyPropertyChanged.PropertyChanged += UpdateJSDataSource;
                }
            }
        }

        private void UpdateProperty(Object obj, PropertyInfo property, object newValue)
        {
            object castedValue = newValue;

            try
            {
                Type propertyType = property.PropertyType;
                if (!propertyType.IsAssignableFrom(newValue.GetType()))
                {
                    bool isCasted = false;
                    if (propertyType.IsEnum)
                    {
                        try
                        {
                            castedValue = Enum.Parse(propertyType, newValue.ToString());
                            isCasted = true;
                        }
                        catch { }
                    }
                    if (!isCasted)
                    {
                        castedValue = Convert.ChangeType(newValue, propertyType);
                    }
                }
                property.SetValue(obj, castedValue, null);
            }
            catch
            {
            }
        }
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshGridDataSource();
        }

        private void InitialiseSearchPanel()
        {
            SearchPanel = new SearchPanelData()
            {
                highlightCaseSensitive = true,
                highlightSearchText = true,
                placeholder = "search",
                searchVisibleColumnsOnly = false,
                text = "",
                visible = true,
                width = 200
            };
        }

        private void InitialiseGroupPanel()
        {
            GroupPanelData = new GroupPanelData()
            {
                allowColumnDragging = true,
                emptyPanelText = "Drag a column header here to group by that column",
                visible = true
            };
        }

        private void InitialiseDataGridSelection()
        {
            DataGridSelection = new dxDataGridSelection()
            {
                allowSelectAll = true,
                mode = "single",
                deferred = false,
                selectAllMode = "page",
                showCheckBoxesMode = "always"
            };
        }

        private void InitialiseDataGridEditing()
        {
            DataGridEditing = new dxDataGridEditing()
            {
                mode = "cell",
                refreshMode = "full",
                selectTextOnEditStart = true,
                startEditAction = "click",
                useIcons = false,
                allowAdding = true,
                allowDeleting = true,
                allowUpdating = true
            };
        }
    }
}
