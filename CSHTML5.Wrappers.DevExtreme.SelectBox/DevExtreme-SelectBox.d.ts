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

declare module DevExpress.ui {
    export interface CollectionWidgetItem {
        /** Specifies whether a widget item should be disabled. */
        disabled?: boolean;
        /** Specifies html code inserted into the widget item element. */
        html?: string;
        /** Specifies a template that should be used to render this item only. */
        //template?: DevExpress.core.template | (() => string | Element | Object);
        /** Specifies text displayed for the widget item. */
        text?: string;
        /** Specifies whether or not a widget item must be displayed. */
        visible?: boolean;
    }

    /** A base class for editors. */
    export class Editor extends Widget {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Resets the value option to the default value. */
        reset(): void;
    }

    /** The base class for widgets. */
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

    /** A drop-down editor widget. */
    export class dxDropDownEditor extends dxTextBox {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Closes the drop-down editor. */
        close(): void;
        /** Opens the drop-down editor. */
        open(): void;
    }

    /** A base class for drop-down list widgets. */
    export class dxDropDownList extends dxDropDownEditor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
    }

    /** The TextBox is a widget that enables a user to enter and edit a single line of text. */
    export class dxTextBox extends dxTextEditor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
    }

    /** A base class for text editing widgets. */
    export class dxTextEditor extends Editor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Removes focus from the input element. */
        blur(): void;
        /** Sets focus to the input element representing the widget. */
        focus(): void;
    }

    /** The SelectBox widget is an editor that allows an end user to select an item from a drop-down list. */
    export class dxSelectBox extends dxDropDownList {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Gets the DataSource instance. */
        getDataSource(): DevExpress.data.DataSource;
    }
}

declare module DevExpress.data {
    /** The DataSource is an object that provides an API for processing data from an underlying store. */
    export class DataSource {
        constructor(data: object);
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