using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.TextBox;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        private String _mask;
        public String Mask
        {
            get
            {
                return _mask;
            }
            set
            {
                _mask = value;

                option("mask", Utils.ToJSObject(_mask));
            }
        }

        private String _maskChar;
        public String MaskChar
        {
            get
            {
                return _maskChar;
            }
            set
            {
                _maskChar = value;

                option("maskChar", Utils.ToJSObject(_maskChar));
            }
        }

        private String _maskInvalidMessage;
        public String MaskInvalidMessage
        {
            get
            {
                return _maskInvalidMessage;
            }
            set
            {
                _maskInvalidMessage = value;

                option("maskInvalidMessage", Utils.ToJSObject(_maskInvalidMessage));
            }
        }

        private Dictionary<String, Regex> _maskRules;
        public Dictionary<String, Regex> MaskRules
        {
            get
            {
                return _maskRules;
            }
            set
            {
                _maskRules = value;

                option("maskRules", Utils.ToJSObject(_maskRules));
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

        private String _showMaskMode;
        public String ShowMaskMode
        {
            get
            {
                return _showMaskMode;
            }
            set
            {
                _showMaskMode = value;

                option("showMaskMode", Utils.ToJSObject(_showMaskMode));
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

        private Boolean _useMaskedValue;
        public Boolean UseMaskedValue
        {
            get
            {
                return _useMaskedValue;
            }
            set
            {
                _useMaskedValue = value;

                option("useMaskedValue", Utils.ToJSObject(_useMaskedValue));
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
				@"var textBoxContainer = $0; textBoxContainer.id = 'textBoxContainer';", 
				(new JSObject(this.DomElement)).ToJavaScriptObject()
			);

			Interop.ExecuteJavaScript(@"$0('#textBoxContainer').dxTextBox({});", _jQueryVersion);

			UnderlyingJSInstance = Interop.ExecuteJavaScript(@"$0('#textBoxContainer').dxTextBox('instance')", _jQueryVersion);

            //Set properties and events globally if needed
           //ShowClearButton = true;
            SetTextBoxOnValueChanged();
            SetTextBoxValueChangeEvent("keyup");

            //note about the comment below: only one occurence of an id should be found in a html document and if there are multiple occurences, only the first can be accessed through its id so we can't use it. (see https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/id)
            //we remove the id since we won't use it anymore and it will let us reuse this id for another dxTextBox:
            Interop.ExecuteJavaScript(
                @"var textBoxContainer = $0; textBoxContainer.id = '';",
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
                                            new Interop.ResourceFile("dx.common", Configuration.LocationOfDXCommonCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/styles/dx.common.css"
                                            new Interop.ResourceFile("dx.theme", Configuration.LocationOfDXThemeCSS), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/styles/dx."theme name".css"
                        },
                        js: new Interop.ResourceFile[]
                        {
                                            new Interop.ResourceFile("jQueryDevExtreme", Configuration.LocationOfJquery), // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/scripts/jquery.min.js"
                                            new Interop.ResourceFile("dx", Configuration.LocationOfDXAllJS) // e.g. "ms-appx:///CSHTML5.Wrappers.DevExtreme.TextBox/scripts/dx.all.js"
                        }
                    );
                }

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

        private void SetTextBoxOnValueChanged()
        {
            Action<String, String> textBoxItems = (Action<String, String>)this.GetCurrentTextBoxItems;

            Interop.ExecuteJavaScript(@"$0.option('onValueChanged', function (e) {
                var previousValue = e.previousValue;

                var newValue = e.value;

                $1(newValue, previousValue);
            });", UnderlyingJSInstance, textBoxItems);
        }

        private void SetTextBoxValueChangeEvent(String eventType)
        {
            Interop.ExecuteJavaScript(@"$0.option('valueChangeEvent', $1);", UnderlyingJSInstance, eventType);
        }

        private String _currentTextBoxValue;
        public String CurrentTextBoxValue
        {
            get
            {
                return _currentTextBoxValue;
            }
        }

        private String _previousTextBoxValue;
        public String PreviousTextBoxValue
        {
            get
            {
                return _previousTextBoxValue;
            }
        }

        public void GetCurrentTextBoxItems(String newValue, String previousValue)
        {
            _currentTextBoxValue = newValue;

            _previousTextBoxValue = previousValue;
        }
    }
}
