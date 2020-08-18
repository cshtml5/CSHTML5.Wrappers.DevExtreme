using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.Switch;
using System;

#if SLMIGRATION
using System.Windows;
#else
using Windows.UI.Xaml;
#endif
using ToJavaScriptObjectExtender;

namespace DevExtreme_Switch.DevExpress.ui
{
	public partial class dxSwitch : JSComponent
	{
		private Boolean _readOnly;
		public Boolean ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly = value;

				option("readOnly", Utils.ToJSObject(_readOnly));
			}
		}

		private Boolean _value;
		public Boolean Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;

				option("value", Utils.ToJSObject(_value));
			}
		}

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
			if(_jQueryVersion == null)
			{
				_jQueryVersion = Interop.ExecuteJavaScript(@"$.noConflict()");
			}

			Interop.ExecuteJavaScript(
				@"var switchContainer = $0; switchContainer.id = 'switchContainer';",
				(new JSObject(this.DomElement)).ToJavaScriptObject()
			);

			Interop.ExecuteJavaScript(@"$0('#switchContainer').dxSwitch({});", _jQueryVersion);

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#switchContainer').dxSwitch('instance')", _jQueryVersion);

			//Set properties and events globally if needed
			//Value = true;

			//note about the comment below: only one occurence of an id should be found in a html document and if there are multiple occurences, only the first can be accessed through its id so we can't use it. (see https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/id)
			//we remove the id since we won't use it anymore and it will let us reuse this id for another dxSwitch:
			Interop.ExecuteJavaScript(
				@"var switchContainer = $0; switchContainer.id = '';",
				(new JSObject(this.DomElement)).ToJavaScriptObject()
			);
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
							new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch/styles/dx.common.css"
							new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch/styles/dx."theme name".css"
						},
						js: new Interop.ResourceFile[]
						{
							new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch/scripts/jquery.min.js"
							new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.Switch/scripts/dx.all.js"
						}
					);
				}

				base.JSComponent_Loaded(sender, e);
                CheckErrorandDisplayItInsteadOfEditorIfNeeded();
			}
			else
			{
				this.Html = @"Before you can use the DevExtreme Switch, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com";
				MessageBox.Show(@"Before you can use the DevExtreme Switch, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com"); //todo: put the address of the tutorial.
				base.AbortLoading();
			}
		}

		async void CheckErrorandDisplayItInsteadOfEditorIfNeeded()
		{
			if (!await this.JSInstanceLoaded)
			{
				this.Html = @"The libraries for the DevExtreme Switch could not be found. Make sure you have added them in your project at the location you specified in the Configuration.";
				MessageBox.Show(@"The libraries for the DevExtreme Switch could not be found. Make sure you have added them in your project at the location you specified in the Configuration.");
			}
		}

	}
}
