using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.TextBox;


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

namespace DevExtreme_TextBox.DevExpress.ui
{
	public partial class dxTextBox : JSComponent
	{
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
				@"var textBoxContainer = $0; textBoxContainer.id = 'textBoxContainer';", 
				(new JSObject(this.DomElement)).ToJavaScriptObject()
			);

			Interop.ExecuteJavaScript(@"$0('#textBoxContainer').dxTextBox({});", _jQueryVersion);

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#textBoxContainer').dxTextBox('instance')", _jQueryVersion);
		}

		protected override void JSComponent_Loaded(object sender, RoutedEventArgs e)
		{
			if (Configuration.AreSourcesSet)
			{
				_jsLibrary = new JSLibrary(
					css: new Interop.ResourceFile[]
					{
						new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/styles/dx.common.css"
                        new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/styles/dx."theme name".css"
                    },
					js: new Interop.ResourceFile[]
					{
						new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/scripts/jquery.min.js"
                        new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/scripts/dx.all.js"
                    }
				);
				base.JSComponent_Loaded(sender, e);
                CheckErrorandDisplayItInsteadOfEditorIfNeeded();
			}
			else
			{
				this.Html = @"Before you can use the DevExtreme TextBox, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com";
				MessageBox.Show(@"Before you can use the DevExtreme TextBox, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com"); //todo: put the address of the tutorial.
				base.AbortLoading();
			}
		}

		async void CheckErrorandDisplayItInsteadOfEditorIfNeeded()
		{
			if (!await this.JSInstanceLoaded)
			{
				this.Html = @"The libraries for the DevExtreme TextBox could not be found. Make sure you have added them in your project at the location you specified in the Configuration.";
				MessageBox.Show(@"The libraries for the DevExtreme TextBox could not be found. Make sure you have added them in your project at the location you specified in the Configuration.");
			}
		}
	}
}
