using CSHTML5;
using CSHTML5.Wrappers.DevExtreme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DevExtreme_DataGrid.DevExpress.ui
{
    /// <summary>
    /// Represents the method that will handle routed events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataGridColumnButtonClickEventHandler(object sender, DataGridColumnButtonClickEventArgs e);

    public class DataGridColumnButtonClickEventArgs
    {
        public DataGridColumnButtonClickEventArgs(object originaljsEventArgs, object item)
        {
            INTERNAL_OriginalJSEventArg = originaljsEventArgs;
            Item = item;
        }

        object _originalJSEventArg;
        /// <summary>
        /// (Optional) Gets the original javascript event arg.
        /// </summary>
        public object INTERNAL_OriginalJSEventArg
        {
            get
            {
                return _originalJSEventArg;
            }
            set
            {
                _originalJSEventArg = value;
            }
        }

        /// <summary>
        /// Gets/sets the item related to the row to which the clicked button belongs.
        /// </summary>
        public object Item { get; set; }
    }

    public partial class dxDataGridColumnButton : GridBaseColumnButton
    {
        /** Allows you to customize buttons in the editing column or create a custom command column. Applies only if the column's type is "buttons". */
        //        export interface dxDataGridColumnButton extends GridBaseColumnButton
        //        {
        //            /** The name used to identify a built-in button. */
        //            name?: 'cancel' | 'delete' | 'edit' | 'save' | 'undelete' | string;
        //        /** A function that is executed when the button is clicked or tapped. Not executed if a template is used. */
        //        onClick?: ((e: { component?: dxDataGrid, element?: DevExpress.core.dxElement, model?: any, event?: DevExpress.events.event, row?: dxDataGridRowObject, column?: dxDataGridColumn }) => any) | string;
        //        /** Specifies a custom button template. */
        //        template?: DevExpress.core.template | ((cellElement: DevExpress.core.dxElement, cellInfo: { component?: dxDataGrid, data?: any, key?: any, columnIndex?: number, column?: dxDataGridColumn, rowIndex?: number, rowType?: string, row?: dxDataGridRowObject
        //    }) => string | Element | JQuery);
        //        /** Specifies the button's visibility. */
        //        visible?: boolean | ((options: { component?: dxDataGrid, row?: dxDataGridRowObject, column?: dxDataGridColumn
        //}) => boolean);
        //    }

        //public dxDataGridColumnButton()
        //{
        //    Initialize();
        //}

        partial void Initialize()
        {
            RegisterToClick();
        }


        internal dxDataGrid _parentDataGrid;

        #region Click event
        //note about this event: we need to register to this event, pre-handle it and only then change the value in c# since the user will probably want the old value if he registers to it.

        /// <summary>
        /// Occurs when the selection is changed.
        /// </summary>
        public event DataGridColumnButtonClickEventHandler Click;

        /// <summary>
        /// Raises the TextChanged event
        /// </summary>
        /// <param name="eventArgs">The arguments for the event.</param>
        protected virtual void OnClick(DataGridColumnButtonClickEventArgs eventArgs)
        {
            if (Click != null)
            {
                Click(this, eventArgs);
            }
        }

        internal void RegisterToClick()
        {
            Interop.ExecuteJavaScript(@"$0.onClick = function(e) {
							$1(e.row);
						}", this.UnderlyingJSInstance, (Action<object>)HandleClick);
        }

        //private void UnregisterFromClick()
        //{
        //	Interop.ExecuteJavaScript("$0.option($1, undefined)", this.UnderlyingJSInstance, "onClick");
        //}

        internal void HandleClick(object clickJSArgs)
        {
            if (Click != null)
            {
                int itemId = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.row.data.DevExtremeId", clickJSArgs));
                object item = _parentDataGrid.GetItemFromId(itemId);
                OnClick(new DataGridColumnButtonClickEventArgs(clickJSArgs, item));
            }
        }

        #endregion
    }
}
