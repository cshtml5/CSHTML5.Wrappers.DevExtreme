interface JQueryPromise<T> {
}

declare module DevExpress {
    export class Component {
        constructor(options?: Object);
        /** Prevents the widget from refreshing until the endUpdate() method is called. */
        beginUpdate(): void;
        /** Refreshes the widget after a call of the beginUpdate() method. */
        endUpdate(): void;
        /** Gets the widget's instance. Use it to access other methods of the widget. */
        instance(): this;
        /** Detaches all event handlers from a single event. */
        off(eventName: string): this;
        /** Detaches a particular event handler from a single event. */
        off(eventName: string, eventHandler: Function): this;
        /** Subscribes to an event. */
        on(eventName: string, eventHandler: Function): this;
        /** Subscribes to events. */
        on(events: any): this;
        /** Gets all widget options. */
        option(): any;
        /** Gets the value of a single option. */
        option(optionName: string): any;
        /** Updates the value of a single option. */
        option(optionName: string, optionValue: any): void;
        /** Updates the values of several options. */
        option(options: any): void;
        /** Resets an option to its default value. */
        resetOption(optionName: string): void;
    }

    export class DOMComponent extends Component {
        constructor(element: Element | Object, options?: Object);
        /** Disposes of all the resources allocated to the widget instance. */
        dispose(): void;
        /** Gets the root widget element. */
        element(): void;
        /** Gets the instance of a widget found using its DOM node. */
        static getInstance(element: Element | Object): DOMComponent;
    }
}

declare module DevExpress.ui
{
    export class Widget extends DOMComponent {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Sets focus on the widget. */
        focus(): void;
        /** Registers a handler to be executed when a user presses a specific key. */
        registerKeyHandler(key: string, handler: Function): void;
        /** Repaints the widget without reloading data. Call it to update the widget's markup. */
        repaint(): void;
    }

    export class GridBase extends Widget {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Shows the load panel. */
        beginCustomLoading(messageText: string): void;
        /** Gets a data object with a specific key. */
        byKey(key: any | string | number): Object;
        /** Discards changes that a user made to data. */
        cancelEditData(): void;
        /** Gets the value of a cell with a specific row index and a data field, column caption or name. */
        cellValue(rowIndex: number, dataField: string): any;
        /** Sets a new value to a cell with a specific row index and a data field, column caption or name. */
        cellValue(rowIndex: number, dataField: string, value: any): void;
        /** Gets the value of a cell with specific row and column indexes. */
        cellValue(rowIndex: number, visibleColumnIndex: number): any;
        /** Sets a new value to a cell with specific row and column indexes. */
        cellValue(rowIndex: number, visibleColumnIndex: number, value: any): void;
        /** Clears all filters applied to widget rows. */
        clearFilter(): void;
        /** Clears all row filters of a specific type. */
        clearFilter(filterName: string): void;
        /** Clears selection of all rows on all pages. */
        clearSelection(): void;
        /** Clears sorting settings of all columns at once. */
        clearSorting(): void;
        /** Switches the cell being edited back to the normal state. Takes effect only if editing.mode is batch and showEditorAlways is false. */
        closeEditCell(): void;
        /** Collapses the currently expanded adaptive detail row (if there is one). */
        collapseAdaptiveDetailRow(): void;
        /** Gets the data column count. Includes visible and hidden columns, excludes command columns. */
        columnCount(): number;
        /** Gets all options of a column with a specific identifier. */
        columnOption(id: number | string): any;
        /** Gets the value of a single column option. */
        columnOption(id: number | string, optionName: string): any;
        /** Updates the value of a single column option. */
        columnOption(id: number | string, optionName: string, optionValue: any): void;
        /** Updates the values of several column options. */
        columnOption(id: number | string, options: any): void;
        /** Removes a column. */
        deleteColumn(id: number | string): void;
        /** Removes a row with a specific index. */
        deleteRow(rowIndex: number): void;
        /** Clears the selection of all rows on all pages or the currently rendered page only. */
        deselectAll(): Object;
        /** Cancels the selection of rows with specific keys. */
        //deselectRows(keys: Array<any>): Object;
        /** Switches a cell with a specific row index and a data field to the editing state. Takes effect only if the editing mode is "batch" or "cell". */
        editCell(rowIndex: number, dataField: string): void;
        /** Switches a cell with specific row and column indexes to the editing state. Takes effect only if the editing mode is "batch" or "cell". */
        editCell(rowIndex: number, visibleColumnIndex: number): void;
        /** Switches a row with a specific index to the editing state. Takes effect only if the editing mode is "row", "popup" or "form". */
        editRow(rowIndex: number): void;
        /** Hides the load panel. */
        endCustomLoading(): void;
        /** Expands an adaptive detail row. */
        expandAdaptiveDetailRow(key: any): void;
        /** Gets a filter expression applied to the widget's data source using the filter(filterExpr) method and the DataSource's filter option. */
        filter(): any;
        /** Applies a filter to the widget's data source. */
        filter(filterExpr: any): void;
        /** Sets focus on the widget. */
        focus(): void;
        /** Sets focus on a specific cell. */
        focus(element: Element | Object): void;
        /** Gets a cell with a specific row index and a data field, column caption or name. */
        getCellElement(rowIndex: number, dataField: string): Object | undefined;
        /** Gets a cell with specific row and column indexes. */
        getCellElement(rowIndex: number, visibleColumnIndex: number): Object | undefined;
        /** Gets the total filter that combines all the filters applied. */
        getCombinedFilter(): any;
        /** Gets the total filter that combines all the filters applied. */
        getCombinedFilter(returnDataField: boolean): any;
        /** Gets the DataSource instance. */
        getDataSource(): Object;
        /** Gets the key of a row with a specific index. */
        getKeyByRowIndex(rowIndex: number): any;
        /** Gets the container of a row with a specific index. */
        getRowElement(rowIndex: number): Object;
        /** Gets the index of a row with a specific key. */
        getRowIndexByKey(key: any | string | number): number;
        /** Gets the instance of the widget's scrollable part. */
        getScrollable(): Object;
        /** Gets the index of a visible column. */
        getVisibleColumnIndex(id: number | string): number;
        /** Checks whether the widget has unsaved changes. */
        hasEditData(): boolean;
        /** Hides the column chooser. */
        hideColumnChooser(): void;
        /** Checks whether an adaptive detail row is expanded or collapsed. */
        isAdaptiveDetailRowExpanded(key: any): boolean;
        /** Checks whether a row with a specific key is focused. */
        isRowFocused(key: any): boolean;
        /** Checks whether a row with a specific key is selected. */
        isRowSelected(key: any): boolean;
        /** Gets a data object's key. */
        keyOf(obj: any): any;
        /** Navigates the grid to the data page that contains the row with the specified key and scrolls the grid to display the row if it is not in the viewport. */
        navigateToRow(key: any): void;
        /** Gets the total page count. */
        pageCount(): number;
        /** Gets the current page index. */
        pageIndex(): number;
        /** Switches the widget to a specific page using a zero-based index. */
        pageIndex(newIndex: number): Object;
        /** Gets the current page size. */
        pageSize(): number;
        /** Sets the page size. */
        pageSize(value: number): void;
        /** Reloads data and repaints the widget. */
        refresh(): Object;
        /** Reloads data and repaints the widget or elements whose data changed. */
        refresh(changesOnly: boolean): Object;
        /** Repaints specific rows. */
        //repaintRows(rowIndexes: Array<number>): void;
        /** Saves changes that a user made to data. */
        saveEditData(): Object;
        /** Seeks a search string in the columns whose allowSearch option is true. */
        searchByText(text: string): void;
        /** Selects all rows. */
        selectAll(): Object;
        /** Selects rows with specific keys. */
        //selectRows(keys: Array<any>, preserve: boolean): Object;
        /** Selects rows with specific indexes. */
        //selectRowsByIndexes(indexes: Array<number>): Object;
        /** Shows the column chooser. */
        showColumnChooser(): void;
        /** Gets the current widget state. */
        state(): any;
        /** Sets the widget state. */
        state(state: any): void;
        /** Recovers a row deleted in batch editing mode. */
        undeleteRow(rowIndex: number): void;
        /** Updates the widget's content after resizing. */
        updateDimensions(): void;
    }

    export class dxDataGrid extends GridBase {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);

        /** Adds a new column. */
        addColumn(columnOptions: any | string): void;
        /** Adds an empty data row and switches it to the editing state. */
        //addRow(): void;
        /** Ungroups grid records. */
        clearGrouping(): void;
        /** Collapses master rows or groups of a specific level. */
        collapseAll(groupIndex?: number): void;
        /** Collapses a group or a master row with a specific key. */
        collapseRow(key: any): Object;
        /** Expands master rows or groups of a specific level. Does not apply if data is remote. */
        expandAll(groupIndex?: number): void;
        /** Expands a group or a master row with a specific key. */
        expandRow(key: any): Object;
        /** Exports grid data to Excel. */
        exportToExcel(selectionOnly: boolean): void;
        /** Gets the currently selected rows' keys. */
        getSelectedRowKeys(): Object;
        /** Gets the selected rows' data objects. */
        getSelectedRowsData(): Object;
        /** Gets the value of a total summary item. */
        getTotalSummaryValue(summaryItemName: string): any;
        /** Gets all visible columns. */
        getVisibleColumns(): Object;
        /** Gets all visible columns at a specific hierarchical level of column headers. Use it to access banded columns. */
        getVisibleColumns(headerLevel: number): Object;
        /** Gets currently rendered rows. */
        getVisibleRows(): Object;
        /** @deprecated Use addRow instead. */
        /** Adds a new data row to a grid. */
        insertRow(): void;
        /** Checks whether a specific group or master row is expanded or collapsed. */
        isRowExpanded(key: any): boolean;
        /** Checks whether a row found using its data object is selected. Takes effect only if selection.deferred is true. */
        isRowSelected(data: any): boolean;
        /** Checks whether a row with a specific key is selected. */
        isRowSelected(key: any): boolean;
        /** @deprecated Use deleteRow instead. */
        option(optionName: string, optionValue: any): void;

        /** Removes a row with a specific index. */
        removeRow(rowIndex: number): void;
        /** Gets the total row count. */
        totalCount(): number;

        /** Updates the value of a single column option. */
        columnOption(id: object, optionName: string, optionValue: any): void;
        /** Updates the values of several column options. */
        columnOption(id: object, options: any): void;

        /** Gets the DataSource instance. */
        getDataSource(): DevExpress.data.DataSource;
    }

    export interface GridBaseColumn {
        /** Aligns the content of the column. */
        alignment?: string;
        /** Specifies whether a user can edit values in the column at runtime. By default, inherits the value of the editing.allowUpdating option. */
        allowEditing?: boolean;
        /** Specifies whether data can be filtered by this column. Applies only if filterRow.visible is true. */
        allowFiltering?: boolean;
        /** Specifies whether a user can fix the column at runtime. Applies only if columnFixing.enabled is true. */
        allowFixing?: boolean;
        /** Specifies whether the header filter can be used to filter data by this column. Applies only if headerFilter.visible is true. By default, inherits the value of the allowFiltering option. */
        allowHeaderFiltering?: boolean;
        /** Specifies whether a user can hide the column using the column chooser at runtime. Applies only if columnChooser.enabled is true. */
        allowHiding?: boolean;
        /** Specifies whether this column can be used in column reordering at runtime. Applies only if allowColumnReordering is true. */
        allowReordering?: boolean;
        /** Specifies whether a user can resize the column at runtime. Applies only if allowColumnResizing is true. */
        allowResizing?: boolean;
        /** Specifies whether this column can be searched. Applies only if searchPanel.visible is true. Inherits the value of the allowFiltering option by default. */
        allowSearch?: boolean;
        /** Specifies whether a user can sort rows by this column at runtime. Applies only if sorting.mode differs from "none". */
        allowSorting?: boolean;
        /** Calculates custom cell values. Use this function to create an unbound data column. */
        calculateCellValue?: object;
        /** Calculates custom display values for column cells. Requires specifying the dataField or calculateCellValue option. Used in lookup optimization. */
        calculateDisplayValue?: object;
        /** Specifies the column's custom filtering rules. */
        calculateFilterExpression?: object;
        /** Calculates custom values to be used in sorting. */
        calculateSortValue?: string | object;
        /** Specifies a caption for the column. */
        caption?: string;
        /** Specifies a CSS class to be applied to the column. */
        cssClass?: string;
        /** Customizes the text displayed in column cells. */
        customizeText?: object;
        /** Binds the column to a field of the dataSource. */
        dataField?: string;
        /** Casts column values to a specific data type. */
        dataType?: string;
        /** Configures the default widget used for editing and filtering in the filter row. */
        editorOptions?: any;
        /** Specifies whether HTML tags are displayed as plain text or applied to the values of the column. */
        encodeHtml?: boolean;
        /** In a boolean column, replaces all false items with a specified text. Applies only if showEditorAlways option is false. */
        falseText?: string;
        /** Specifies a set of available filter operations. Applies only if filterRow.visible and allowFiltering are true. */
        filterOperations?: object;
        /** Specifies whether a user changes the current filter by including (selecting) or excluding (clearing the selection of) values. Applies only if headerFilter.visible and allowHeaderFiltering are true. */
        filterType?: string;
        /** Specifies the column's filter value displayed in the filter row. */
        filterValue?: any;
        /** Specifies values selected in the column's header filter. */
        filterValues?: object;
        /** Fixes the column. */
        fixed?: boolean;
        /** Specifies the widget's edge to which the column is fixed. Applies only if columns[].fixed is true. */
        fixedPosition?: string;
        /** Configures the form item that the column produces in the editing state. Applies only if editing.mode is "form" or "popup". */
        formItem?: dxFormSimpleItem;
        /** Formats a value before it is displayed in a column cell. */
        format?: format;
        /** Specifies data settings for the header filter. */
        //headerFilter?: { allowSearch?: boolean, dataSource?: Array<any> | ((options: { component?: any, dataSource?: DevExpress.data.DataSourceOptions }) => any) | DevExpress.data.DataSourceOptions, groupInterval?: 'day' | 'hour' | 'minute' | 'month' | 'quarter' | 'second' | 'year' | number, height?: number, searchMode?: 'contains' | 'startswith' | 'equals', width?: number };
        /** Specifies the order in which columns are hidden when the widget adapts to the screen or container size. Ignored if allowColumnResizing is true and columnResizingMode is "widget". */
        hidingPriority?: number;
        /** Specifies whether the column bands other columns or not. */
        isBand?: boolean;
        /** Specifies options of a lookup column. */
        //lookup?: { allowClearing?: boolean, dataSource?: Array<any> | DevExpress.data.DataSourceOptions | DevExpress.data.Store | ((options: { data?: any, key?: any }) => Array<any> | DevExpress.data.DataSourceOptions | DevExpress.data.Store), displayExpr?: string | ((data: any) => string), valueExpr?: string };
        /** Specifies the minimum width of the column. */
        minWidth?: number;
        /** Specifies the identifier of the column. */
        name?: string;
        /** Specifies the band column that owns the current column. Accepts the index of the band column in the columns array. */
        ownerBand?: number;
        /** Specifies whether to render the column after other columns and elements. Use if column cells have a complex template. Requires the width option specified. */
        renderAsync?: boolean;
        /** Specifies the column's filter operation displayed in the filter row. */
        //selectedFilterOperation?: '<' | '<=' | '<>' | '=' | '>' | '>=' | 'between' | 'contains' | 'endswith' | 'notcontains' | 'startswith';
        /** Specifies a function to be invoked after the user has edited a cell value, but before it will be saved in the data source. */
        //setCellValue?: ((newData: any, value: any, currentRowData: any) => void | Promise<void> | JQueryPromise<void>);
        /** Specifies whether the column displays its values using editors. */
        showEditorAlways?: boolean;
        /** Specifies whether the column chooser can contain the column header. */
        showInColumnChooser?: boolean;
        /** Specifies the index according to which columns participate in sorting. */
        sortIndex?: number;
        /** Specifies the sort order of column values. */
        sortOrder?: string | undefined;
        /** Specifies a custom comparison function for sorting. Applies only when sorting is performed on the client. */
        //sortingMethod?: ((value1: any, value2: any) => number);
        /** In a boolean column, replaces all true items with a specified text. Applies only if showEditorAlways option is false. */
        trueText?: string;
        /** Specifies validation rules to be checked when cell values are updated. */
        //validationRules?: Array<RequiredRule | NumericRule | RangeRule | StringLengthRule | CustomRule | CompareRule | PatternRule | EmailRule | AsyncRule>;
        /** Specifies whether the column is visible, that is, occupies space in the table. */
        visible?: boolean;
        /** Specifies the position of the column regarding other columns in the resulting widget. */
        visibleIndex?: number;
        /** Specifies the column's width in pixels or as a percentage. Ignored if it is less than minWidth. */
        width?: number | string;
    }

    /** Allows you to customize buttons in the editing column or create a custom command column. Applies only if the column's type is "buttons". */
    export interface GridBaseColumnButton {
        /** Specifies a CSS class to be applied to the button. */
        cssClass?: string;
        /** Specifies the text for the hint that appears when the button is hovered over or long-pressed. */
        hint?: string;
        /** Specifies the button's icon. */
        icon?: string;
        /** Specifies the button's text. Applies only if the button's icon is not specified. */
        text?: string;
    }

    export interface dxDataGridColumnButton { }

    export interface dxDataGridColumn extends GridBaseColumn {
        /*inherited from GridBaseColumn*/
        /** Aligns the content of the column. */
        alignment?: string;
        /** Specifies a caption for the column. */
        caption?: string;
        /** Binds the column to a field of the dataSource. */
        dataField?: string;
        /** Casts column values to a specific data type. */
        dataType?: string;
        /** Formats a value before it is displayed in a column cell. */
        format?: string;

        ///** Specifies whether data from this column should be exported. Applies only if the column is visible. */
        //allowExporting?: boolean;
        /** Specifies whether the user can group data by values of this column. Applies only when grouping is enabled. */
        allowGrouping?: boolean;
        /** Specifies whether groups appear expanded or not when records are grouped by a specific column. Setting this option makes sense only when grouping is allowed for this column. */
        autoExpandGroup?: boolean;
        /** Allows you to customize buttons in the editing column or create a custom command column. Applies only if the column's type is "buttons". */
        buttons?: object; //dxDataGridColumnButton[];
        ///** Specifies a field name or a function that returns a field name or a value to be used for grouping column cells. */
        ////calculateGroupValue?: string | ((rowData: any) => any);
        ///** Specifies a custom template for data cells. */
        ////cellTemplate?: DevExpress.core.template | ((cellElement: DevExpress.core.dxElement, cellInfo: { data?: any, component?: dxDataGrid, value?: any, oldValue?: any, displayValue?: any, text?: string, columnIndex?: number, rowIndex?: number, column?: dxDataGridColumn, row?: dxDataGridRowObject, rowType?: string, watch?: Function }) => any);
        ///** An array of grid columns. */
        ////columns?: Array<dxDataGridColumn | string>;
        ///** Specifies a custom template for data cells in editing state. */
        ////editCellTemplate?: DevExpress.core.template | ((cellElement: DevExpress.core.dxElement, cellInfo: { setValue?: any, data?: any, component?: dxDataGrid, value?: any, displayValue?: any, text?: string, columnIndex?: number, rowIndex?: number, column?: dxDataGridColumn, row?: dxDataGridRowObject, rowType?: string, watch?: Function }) => any);
        ///** Specifies a custom template for group cells (group rows). */
        ////groupCellTemplate?: DevExpress.core.template | ((cellElement: DevExpress.core.dxElement, cellInfo: { data?: any, component?: dxDataGrid, value?: any, text?: string, displayValue?: any, columnIndex?: number, rowIndex?: number, column?: dxDataGridColumn, row?: dxDataGridRowObject, summaryItems?: Array<any>, groupContinuesMessage?: string, groupContinuedMessage?: string }) => any);
        /** Specifies the index of a column when grid records are grouped by the values of this column. */
        groupIndex?: number;
        ///** Specifies a custom template for column headers. */
        ////headerCellTemplate?: DevExpress.core.template | ((columnHeader: DevExpress.core.dxElement, headerInfo: { component?: dxDataGrid, columnIndex?: number, column?: dxDataGridColumn }) => any);
        ///** Specifies whether or not to display the column when grid records are grouped by it. */
        //showWhenGrouped?: boolean;
        /** Specifies the command column that this object customizes. */
        type?: string;
    }

    export interface dxDataGridSelection extends GridBaseSelection {
        /** Specifies whether a user can select all rows at once. */
        allowSelectAll?: boolean;
        /** Specifies the selection mode. */
        mode?: string;
        /** Makes selection deferred. */
        deferred?: boolean;
        /** Specifies the mode in which all the records are selected. Applies only if selection.allowSelectAll is true. */
        selectAllMode?: string;
        /** Specifies when to display check boxes in rows. Applies only if selection.mode is "multiple". */
        showCheckBoxesMode?: string;
    }

    export interface GridBaseEditing {
        /** Configures the form. Used only if editing.mode is "form" or "popup". */
        form?: object;
        /** Specifies how a user edits data. */
        mode?: string; //'batch' | 'cell' | 'row' | 'form' | 'popup'
        /** Configures the popup. Used only if editing.mode is "popup". */
        popup?: object;
        /** Specifies operations that are performed after saving changes. */
        refreshMode?: string; // 'full' | 'reshape' | 'repaint'
        /** Specifies whether to select text in a cell when a user starts editing. */
        selectTextOnEditStart?: boolean;
        /** Specifies whether a single or double click should switch a cell to the editing state. Applies if editing.mode is "cell" or "batch". */
        startEditAction?: string; //'click' | 'dblClick'
        /** Contains options that specify texts for editing-related UI elements. */
        texts?: object;
        /** Specifies whether the editing column uses icons instead of links. */
        useIcons?: boolean;
    }

    export interface dxDataGridEditing extends GridBaseEditing {

        /** Specifies how a user edits data. */
        mode?: string; //'batch' | 'cell' | 'row' | 'form' | 'popup'
        /** Specifies operations that are performed after saving changes. */
        refreshMode?: string; // 'full' | 'reshape' | 'repaint'
        /** Specifies whether to select text in a cell when a user starts editing. */
        selectTextOnEditStart?: boolean;
        /** Specifies whether a single or double click should switch a cell to the editing state. Applies if editing.mode is "cell" or "batch". */
        startEditAction?: string; //'click' | 'dblClick'
        /** Specifies whether the editing column uses icons instead of links. */
        useIcons?: boolean;

        /** Specifies whether a user can add new rows. */
        allowAdding?: boolean;
        /** Specifies whether a user can delete rows. It is called for each data row when defined as a function. */
        allowDeleting?: boolean;
        /** Specifies whether a user can update rows. It is called for each data row when defined as a function. */
        allowUpdating?: boolean;
    }
}

declare module DevExpress.data {

    /** Configures the search panel. */
    export class SearchPanelData {
        highlightCaseSensitive?: boolean;
        highlightSearchText?: boolean;
        placeholder?: string;
        searchVisibleColumnsOnly?: boolean;
        text?: string;
        visible?: boolean;
        width?: number;
    };

    export class GroupPanelData {
        allowColumnDragging?: boolean;
        emptyPanelText?: string;
        visible?: boolean;
    };


    /** The DataSource is an object that provides an API for processing data from an underlying store. */
    export class DataSource {
        constructor(data: object);
        constructor(options: CustomStoreOptions | DataSourceOptions);
        constructor(store: Store);
        constructor(url: string);
        /** Cancels the load operation with a specific identifier. */
        cancel(): boolean;
        /** Disposes of all the resources allocated to the DataSource instance. */
        dispose(): void;
        /** Gets the filter option's value. */
        filter(): any;
        /** Sets the filter option's value. */
        filter(filterExpr: any): void;
        /** Gets the group option's value. */
        group(): any;
        /** Sets the group option's value. */
        group(groupExpr: any): void;
        /** Checks whether the count of items on the current page is less than the pageSize. Takes effect only with enabled paging. */
        isLastPage(): boolean;
        /** Checks whether data is loaded in the DataSource. */
        isLoaded(): boolean;
        /** Checks whether data is being loaded in the DataSource. */
        isLoading(): boolean;
        ///** Gets data items the DataSource performs operations on. */
        items(): object;
        /** Gets the value of the underlying store's key option. */
        key(): object;
        /** Starts loading data. */
        load(): object;
        /** Gets an object with current data processing settings. */
        loadOptions(): any;
        /** Detaches all event handlers from a single event. */
        off(eventName: string): this;
        /** Detaches a particular event handler from a single event. */
        off(eventName: string, eventHandler: Function): this;
        /** Subscribes to an event. */
        on(eventName: string, eventHandler: Function): this;
        /** Subscribes to events. */
        on(events: any): this;
        /** Gets the current page index. */
        pageIndex(): number;
        /** Sets the index of the page that should be loaded on the next load() method call. */
        pageIndex(newIndex: number): void;
        /** Gets the page size. */
        pageSize(): number;
        /** Sets the page size. */
        pageSize(value: number): void;
        /** Gets the paginate option's value. */
        paginate(): boolean;
        /** Sets the paginate option's value. */
        paginate(value: boolean): void;
        /** Clears currently loaded DataSource items and calls the load() method. */
        reload(): object;
        /** Gets the requireTotalCount option's value. */
        requireTotalCount(): boolean;
        /** Sets the requireTotalCount option's value. */
        requireTotalCount(value: boolean): void;
        /** Gets the searchExpr option's value. */
        searchExpr(): object;
        /** Sets the searchExpr option's value. */
        searchExpr(expr: string | Function | object): void;
        /** Gets the searchOperation option's value. */
        searchOperation(): string;
        /** Sets the searchOperation option's value. */
        searchOperation(op: string): void;
        /** Gets the searchValue option's value. */
        searchValue(): any;
        /** Sets the searchValue option's value. */
        searchValue(value: any): void;
        /** Gets the select option's value. */
        select(): any;
        /** Sets the select option's value. */
        select(expr: any): void;
        /** Gets the sort option's value. */
        sort(): any;
        /** Sets the sort option's value. */
        sort(sortExpr: any): void;
        /** Gets the instance of the store underlying the DataSource. */
        store(): any;
        /** Gets the number of data items in the store after the last load() operation without paging. Takes effect only if requireTotalCount is true */
        totalCount(): number;
    }
}