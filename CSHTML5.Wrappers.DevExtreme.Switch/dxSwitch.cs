using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.Switch;
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

namespace DevExtreme_Switch.DevExpress.ui
{
	public partial class dxSwitch : JSComponent
	{
		//public bool Value
		//{
		//	get { return (bool)GetValue(ValueProperty); }
		//	set { SetValue(ValueProperty, value); }
		//}

		//// Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty ValueProperty =
		//	DependencyProperty.Register("Value", typeof(bool), typeof(dxSwitch), new PropertyMetadata(false, Value_Changed));

		//private static void Value_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	bool newValue = (bool)e.NewValue;
		//	dxSwitch dxswitchInstance = (dxSwitch)d;
		//	dxswitchInstance.option("value", JSObject.CreateFrom(newValue));
		//}

		public bool Value
		{
			get
			{
				return Convert.ToBoolean(Interop.ExecuteJavaScript("$0._options.value", UnderlyingJSInstance));
			}
			set
			{
				//this.option("value", JSObject.CreateFrom(value));
				Interop.ExecuteJavaScript("$0.option('value', $1)", UnderlyingJSInstance, value);
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

			Interop.ExecuteJavaScript(@"
var gridContainer = $0;
gridContainer.id = 'gridContainer';
", (new JSObject(this.DomElement)).ToJavaScriptObject());

			Interop.ExecuteJavaScript(@"$0('#gridContainer').dxSwitch({});", _jQueryVersion);

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#gridContainer').dxSwitch('instance')", _jQueryVersion);

			//var switchOptions = new dxSwitchOptions()
			//{
			//	//switchedOnText = "I am ON",
			//	//switchedOffText = "I am OFF",
			//	onValueChanged = (Action<object>)(e2 =>
			//	{
			//		MessageBox.Show("Value has changed");
			//		bool actualValue = Convert.ToBoolean(Interop.ExecuteJavaScript("$0._options.value", UnderlyingJSInstance));
			//		this.SetValue(ValueProperty, actualValue);
			//	})
			//};
			//this.option(switchOptions);
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
						new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/jquery.min.js"
                        new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.DataGrid/scripts/dx.all.js"
                    }
				);
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
