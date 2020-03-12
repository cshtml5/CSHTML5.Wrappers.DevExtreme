﻿using CSHTML5;
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

using Windows.UI.Xaml;
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
				SetSearchPanelData(_searchPanel);
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
				SetGroupPanelData(_groupPanelData);
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
				SetDataGridSelectionData(_dataGridSelection);
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
				SetDataGridEditing(_dataGridEditing);
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

		//public object SelectedItem
		//{
		//	get
		//	{
		//		return null;
		//	}
		//}

		//public List<object> SelectedItems
		//{
		//	get
		//	{
		//		List<object> selectedItems = new List<object>();
		//		var itemsList = Interop.ExecuteJavaScript(@"$0.getSelectedRowsData()", UnderlyingJSInstance);
		//		int itemListLength = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.length", itemsList));
		//		for (int i = 0; i < itemListLength; i++)
		//		{
		//			selectedItems.Add(Interop.ExecuteJavaScript(@"$0[$1]", itemsList, i));
		//		}
		//		return selectedItems;
		//	}
		//}


		public static Configuration Configuration = new Configuration();

		static JSLibrary _jsLibrary;
		public override JSLibrary JSLibrary { get { return _jsLibrary; } }

		partial void Initialize()
		{
			base.Initialize(initJSInstance: true);
		}
		protected override void InitializeJSInstance()
		{
			Interop.ExecuteJavaScript(@"
var gridContainer = $0;
gridContainer.id = 'gridContainer';
", (new JSObject(this.DomElement)).ToJavaScriptObject());

			Interop.ExecuteJavaScript(@"jQuery('#gridContainer').dxDataGrid({});");

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"jQuery('#gridContainer').dxDataGrid('instance')");
			ValueModifiedCallback();
			RowRemovedCallback();
			RowAddedCallback();

			InitialiseDataGridEditing();
			InitialiseDataGridSelection();
			InitialiseGroupPanel();
			InitialiseSearchPanel();

			RowAlternationEnabled = true;
			ShowBorders = true;
			AllowColumnReordering = true;
		}

		protected override void JSComponent_Loaded(object sender, RoutedEventArgs e)
		{
			if (Configuration.AreSourcesSet)
			{
				_jsLibrary = new JSLibrary(
					css: new Interop.ResourceFile[]
					{
						new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/styles/dx.common.css"
                        new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/styles/dx."theme name".css"
                    },
					js: new Interop.ResourceFile[]
					{
						new Interop.ResourceFile("jquery", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/jquery.min.js"
                        new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/dx.all.js"
                    }
				);
				base.JSComponent_Loaded(sender, e);
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
						data.PropertyChanged += UpdateJSData;
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
						data.PropertyChanged -= UpdateJSData;
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

				if(objectToAddType == dataSourceType)
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

		public void SetSearchPanelData(SearchPanelData searchPanelData)
		{
			option("searchPanel", searchPanelData);
		}

		public void SetGroupPanelData(GroupPanelData groupPanelData)
		{
			option("groupPanel", groupPanelData);
		}

		public void SetDataGridSelectionData(dxDataGridSelection dataGridSelection)
		{
			option("selection", dataGridSelection);
		}

		public void SetDataGridEditing(dxDataGridEditing gridBaseEditing)
		{
			option("editing", gridBaseEditing);
		}

		private void ValueModifiedCallback()
		{
			Action<int, string, object> updateSource = (Action<int, string, object>)this.UpdateSource;

			Interop.ExecuteJavaScript(@"$0.option('onEditorPrepared', function (options) {
                if (options.parentType == 'dataRow')
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

		private void UpdateJSData(object sender, PropertyChangedEventArgs e)
		{
			int objectID = _itemsToIdsDictionary[sender];

			Type objectType = sender.GetType();
			PropertyInfo property = objectType.GetProperty(e.PropertyName);

			_plainDataSource[objectID][e.PropertyName] = property.GetValue(sender);

			var JSObject = Utils.ConvertPlainDataSourceToJson(_plainDataSource, PropertyNames, ITEMS_ID_NAME_IN_JSON);

			DataSource dataSource = new DataSource(JSObject);

			option("dataSource", dataSource);
		}

		private void RowRemovedCallback()
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
				objectToRemoveAsINotifyPropertyChanged.PropertyChanged -= UpdateJSData;
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

		private void RowAddedCallback()
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
					newObjectAsINotifyPropertyChanged.PropertyChanged += UpdateJSData;
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
