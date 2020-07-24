//interface JQueryPromise<T> {
//}

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
    ////1881
    //export interface EditorOptions<T = Editor> {
    //    /** Specifies or indicates whether the editor's value is valid. */
    //    isValid?: boolean;
    //    ///** A function that is executed after the widget's value is changed. */
    //    //onValueChanged?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, value?: any, previousValue?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    /** Specifies whether the editor is read-only. */
    //    readOnly?: boolean;
    //    /** Information on the broken validation rule. Contains the first item from the validationErrors array. */
    //    validationError?: any;
    //    /** An array of the validation rules that failed. */
    //    validationErrors?: Array<any>;
    //    /** Specifies how the message about the validation rules that are not satisfied by this editor's value is displayed. */
    //    validationMessageMode?: 'always' | 'auto';
    //    /** Indicates or specifies the current validation status. */
    //    validationStatus?: 'valid' | 'invalid' | 'pending';
    //    /** Specifies the widget's value. */
    //    value?: any;
    //}

    //1899
    /** A base class for editors. */
    export class Editor extends Widget {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Resets the value option to the default value. */
        reset(): void;
    }

    ////2488
    //export interface WidgetOptions<T = Widget> {
    //    /** Specifies the shortcut key that sets focus on the widget. */
    //    accessKey?: string;
    //    /** Specifies whether or not the widget changes its state when interacting with a user. */
    //    activeStateEnabled?: boolean;
    //    /** Specifies whether the widget responds to user interaction. */
    //    disabled?: boolean;
    //    /** Specifies whether the widget can be focused using keyboard navigation. */
    //    focusStateEnabled?: boolean;
    //    /** Specifies text for a hint that appears when a user pauses on the widget. */
    //    hint?: string;
    //    /** Specifies whether the widget changes its state when a user pauses on it. */
    //    hoverStateEnabled?: boolean;
    //    /** A function that is executed when the widget's content is ready and each time the content is changed. */
    //    //onContentReady?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any }) => any);
    //    /** Specifies the number of the element when the Tab key is used for navigating. */
    //    tabIndex?: number;
    //    /** Specifies whether the widget is visible. */
    //    visible?: boolean;
    //}

    //2509
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

    ////5704
    //export interface dxTextBoxOptions<T = dxTextBox> extends dxTextEditorOptions<T> {
    //    /** Specifies the maximum number of characters you can enter into the textbox. */
    //    maxLength?: string | number;
    //    /** The "mode" attribute value of the actual HTML input element representing the text box. */
    //    mode?: 'email' | 'password' | 'search' | 'tel' | 'text' | 'url';
    //    /** Specifies a value the widget displays. */
    //    value?: string;
    //}

    //5712
    /** The TextBox is a widget that enables a user to enter and edit a single line of text. */
    export class dxTextBox extends dxTextEditor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
    }

    ////5718
    //export interface dxTextEditorOptions<T = dxTextEditor> {
    //    /** Allows you to add custom buttons to the input text field. */
    //    buttons?: Array<string | 'clear'>;
    //    /** Specifies whether the widget can be focused using keyboard navigation. */
    //    focusStateEnabled?: boolean;
    //    /** Specifies whether the widget changes its state when a user pauses on it. */
    //    hoverStateEnabled?: boolean;
    //    /** Specifies the attributes to be passed on to the underlying HTML element. */
    //    inputAttr?: any;
    //    /** The editor mask that specifies the custom format of the entered string. */
    //    mask?: string;
    //    /** Specifies a mask placeholder. A single character is recommended. */
    //    maskChar?: string;
    //    /** A message displayed when the entered text does not match the specified pattern. */
    //    maskInvalidMessage?: string;
    //    /** Specifies custom mask rules. */
    //    maskRules?: any;
    //    /** The value to be assigned to the `name` attribute of the underlying HTML element. */
    //    name?: string;
    //    ///** A function that is executed when the widget loses focus after the text field's content was changed using the keyboard. */
    //    //onChange?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the widget's input has been copied. */
    //    //onCopy?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the widget's input has been cut. */
    //    //onCut?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the Enter key has been pressed while the widget is focused. */
    //    //onEnterKey?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the widget gets focus. */
    //    //onFocusIn?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the widget loses focus. */
    //    //onFocusOut?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed each time the widget's input is changed while the widget is focused. */
    //    //onInput?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when a user is pressing a key on the keyboard. */
    //    //onKeyDown?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when a user presses a key on the keyboard. */
    //    //onKeyPress?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when a user releases a key on the keyboard. */
    //    //onKeyUp?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    ///** A function that is executed when the widget's input has been pasted. */
    //    //onPaste?: ((e: { component?: T, element?: DevExpress.core.dxElement, model?: any, jQueryEvent?: JQueryEventObject, event?: event }) => any);
    //    /** The text displayed by the widget when the widget value is empty. */
    //    placeholder?: string;
    //    /** Specifies whether to display the Clear button in the widget. */
    //    showClearButton?: boolean;
    //    /** Specifies when the widget shows the mask. Applies only if useMaskedValue is true. */
    //    showMaskMode?: 'always' | 'onFocus';
    //    /** Specifies whether or not the widget checks the inner text for spelling mistakes. */
    //    spellcheck?: boolean;
    //    /** Specifies how the widget's text field is styled. */
    //    stylingMode?: 'outlined' | 'underlined' | 'filled';
    //    /** The read-only option that holds the text displayed by the widget input element. */
    //    text?: string;
    //    /** Specifies whether the value should contain mask characters or not. */
    //    useMaskedValue?: boolean;
    //    /** Specifies the current value displayed by the widget. */
    //    value?: any;
    //    /** Specifies the DOM events after which the widget's value should be updated. */
    //    valueChangeEvent?: string;
    //}

    //5778
    /** A base class for text editing widgets. */
    export class dxTextEditor extends Editor {
        constructor(element: Element, options?: Object);
        constructor(element: Object, options?: Object);
        /** Removes focus from the input element. */
        blur(): void;
        /** Sets focus to the input element representing the widget. */
        focus(): void;
        /** Gets an instance of a custom action button. */
        //getButton(name: string): dxButton | undefined;
    }
}

declare module DevExpress.data {
}