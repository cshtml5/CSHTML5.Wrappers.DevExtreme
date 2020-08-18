using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.NumberBox;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#if SLMIGRATION
using System.Windows;
#else
using Windows.UI.Xaml;
#endif
using ToJavaScriptObjectExtender;

namespace DevExtreme_NumberBox.DevExpress.ui
{
	public partial class dxNumberBox : JSComponent
	{
        private Boolean _disabled;
        public Boolean Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                _disabled = value;

                option("disabled", Utils.ToJSObject(_disabled));
            }
        }

        private String _format;
        public String Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;

                option("format", Utils.ToJSObject(_format));
            }
        }

        private Boolean _isValid;
        public Boolean IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;

                option("isValid", Utils.ToJSObject(_isValid));
            }
        }

        private String _mode;
        public String Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;

                option("mode", Utils.ToJSObject(_mode));
            }
        }

        private String _placeholder;
        public String Placeholder
        {
            get
            {
                return _placeholder;
            }
            set
            {
                _placeholder = value;

                option("placeholder", Utils.ToJSObject(_placeholder));
            }
        }

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

        private Boolean _showClearButton;
        public Boolean ShowClearButton
        {
            get
            {
                return _showClearButton;
            }
            set
            {
                _showClearButton = value;

                option("showClearButton", Utils.ToJSObject(_showClearButton));
            }
        }

        private Boolean _showSpinButtons;
        public Boolean ShowSpinButtons
        {
            get
            {
                return _showSpinButtons;
            }
            set
            {
                _showSpinButtons = value;

                option("showSpinButtons", Utils.ToJSObject(_showSpinButtons));
            }
        }

        private Int32 _tabIndex;
        public Int32 TabIndex
        {
            get
            {
                return _tabIndex;
            }
            set
            {
                _tabIndex = value;

                option("tabIndex", Utils.ToJSObject(_tabIndex));
            }
        }

        private String _value;
        public String Value
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

        private String _valueChangeEvent;
        public String ValueChangeEvent
        {
            get
            {
                return _valueChangeEvent;
            }
            set
            {
                _valueChangeEvent = value;

                option("valueChangeEvent", Utils.ToJSObject(_valueChangeEvent));
            }
        }

        private Boolean _visible;
        public Boolean Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;

                option("visible", Utils.ToJSObject(_visible));
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
				@"var NumberBoxContainer = $0; NumberBoxContainer.id = 'NumberBoxContainer';", 
				(new JSObject(this.DomElement)).ToJavaScriptObject()
			);

			Interop.ExecuteJavaScript(@"$0('#NumberBoxContainer').dxNumberBox({});", _jQueryVersion);

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#NumberBoxContainer').dxNumberBox('instance')", _jQueryVersion);

            //Set properties and events globally if needed
           //ShowClearButton = true;
            SetNumberBoxOnValueChanged();
            SetNumberBoxValueChangeEvent("keyup");

            //note about the comment below: only one occurence of an id should be found in a html document and if there are multiple occurences, only the first can be accessed through its id so we can't use it. (see https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/id)
            //we remove the id since we won't use it anymore and it will let us reuse this id for another dxNumberBox:
            Interop.ExecuteJavaScript(
                @"var NumberBoxContainer = $0; NumberBoxContainer.id = '';",
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
                                            new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox/styles/dx.common.css"
                                            new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox/styles/dx."theme name".css"
                        },
                        js: new Interop.ResourceFile[]
                        {
                                            new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox/scripts/jquery.min.js"
                                            new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.NumberBox/scripts/dx.all.js"
                        }
                    );
                }

				base.JSComponent_Loaded(sender, e);
                CheckErrorandDisplayItInsteadOfEditorIfNeeded();
			}
			else
			{
				this.Html = @"Before you can use the DevExtreme NumberBox, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com";
				MessageBox.Show(@"Before you can use the DevExtreme NumberBox, you must add to your project the corresponding libraries.
To do so, please follow the tutorial at: http://www.cshtml5.com"); //todo: put the address of the tutorial.
				base.AbortLoading();
			}
		}

		async void CheckErrorandDisplayItInsteadOfEditorIfNeeded()
		{
			if (!await this.JSInstanceLoaded)
			{
				this.Html = @"The libraries for the DevExtreme NumberBox could not be found. Make sure you have added them in your project at the location you specified in the Configuration.";
				MessageBox.Show(@"The libraries for the DevExtreme NumberBox could not be found. Make sure you have added them in your project at the location you specified in the Configuration.");
			}
		}

        private void SetNumberBoxOnValueChanged()
        {
            Action<String, String> NumberBoxItems = (Action<String, String>)this.GetCurrentNumberBoxItems;

            Interop.ExecuteJavaScript(@"$0.option('onValueChanged', function (e) {
                var previousValue = e.previousValue;

                var newValue = e.value;

                $1(newValue, previousValue);
            });", UnderlyingJSInstance, NumberBoxItems);
        }

        private void SetNumberBoxValueChangeEvent(String eventType)
        {
            Interop.ExecuteJavaScript(@"$0.option('valueChangeEvent', $1);", UnderlyingJSInstance, eventType);
        }

        private String _currentNumberBoxValue;
        public String CurrentNumberBoxValue
        {
            get
            {
                return _currentNumberBoxValue;
            }
        }

        private String _previousNumberBoxValue;
        public String PreviousNumberBoxValue
        {
            get
            {
                return _previousNumberBoxValue;
            }
        }

        public void GetCurrentNumberBoxItems(String newValue, String previousValue)
        {
            _currentNumberBoxValue = newValue;

            _previousNumberBoxValue = previousValue;
        }
    }
}
