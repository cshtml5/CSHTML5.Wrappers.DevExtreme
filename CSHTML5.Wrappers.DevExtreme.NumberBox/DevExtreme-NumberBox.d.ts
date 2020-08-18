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
    /** A base class for editors. */
    export class Editor extends Widget {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Resets the value option to the default value. */
        reset(): void;
    }

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

    //4617
    /** The NumberBox is a widget that displays a numeric value and allows a user to modify it by typing in a value, and incrementing or decrementing it using the keyboard or mouse. */
    export class dxNumberBox extends dxTextEditor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
    }

    ///** The TextBox is a widget that enables a user to enter and edit a single line of text. */
    //export class dxTextBox extends dxTextEditor {
    //    constructor(element: Element, options?: Object);
    //    constructor(element: Object, options?: Object);
    //}

    /** A base class for text editing widgets. */
    export class dxTextEditor extends Editor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Removes focus from the input element. */
        blur(): void;
        /** Sets focus to the input element representing the widget. */
        focus(): void;
    }
}

declare module DevExpress.data {
}