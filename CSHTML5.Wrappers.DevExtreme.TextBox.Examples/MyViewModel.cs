using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CSHTML5.Wrappers.DevExtreme.TextBox.Examples
{
    class MyViewModel : INotifyPropertyChanged
    {
        public MyViewModel()
        {
            TestProperty = "Default Value Set From Class Contructor";
        }

        public String TestProperty = "Default Value Set From Class";

        private String _testProperty2 = String.Empty;
        public String TestProperty2
        {
            get
            {
                return _testProperty2;
            }

            set
            {
                _testProperty2 = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
