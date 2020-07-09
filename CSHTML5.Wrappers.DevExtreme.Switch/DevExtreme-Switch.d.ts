//declare module DevExpress {
//    export class Component {
//        /** Updates the value of a single option. */
        
//    }
//    export class DOMComponent extends Component {
//        //constructor(element: Element | Object, options?: Object);
//        ///** Disposes of all the resources allocated to the widget instance. */
//        //dispose(): void;
//        ///** Gets the root widget element. */
//        //element(): void;
//        ///** Gets the instance of a widget found using its DOM node. */
//        //static getInstance(element: Element | Object): DOMComponent;
//    }
//}

declare module DevExpress.ui {
    //export class Widget extends DOMComponent {
    //    ////constructor(element: Element, options?: Object);
    //    ////constructor(element: Object, options?: Object);
    //    ///** Sets focus on the widget. */
    //    //focus(): void;
    //    ///** Registers a handler to be executed when a user presses a specific key. */
    //    //registerKeyHandler(key: string, handler: Function): void;
    //    ///** Repaints the widget without reloading data. Call it to update the widget's markup. */
    //    //repaint(): void;
    //}
    ///** A base class for editors. */
    //export class Editor extends Widget {
    //    //constructor(element: Element, options?: EditorOptions)
    //    //constructor(element: JQuery, options?: EditorOptions)
    //    /** Resets the value option to the default value. */
    //    //reset(): void;
    //}
    export interface dxSwitchOptions {
        /** Specifies whether or not the widget changes its state when interacting with a user. */
        activeStateEnabled?: boolean;
        /** Specifies whether the widget can be focused using keyboard navigation. */
        focusStateEnabled?: boolean;
        /** Specifies whether the widget changes its state when a user pauses on it. */
        hoverStateEnabled?: boolean;
        /** The value to be assigned to the `name` attribute of the underlying HTML element. */
        name?: string;
        /** @deprecated Use the switchedOffText option instead. */
        /** Text displayed when the widget is in a disabled state. */
        offText?: string;
        /** @deprecated Use the switchedOnText option instead. */
        /** Text displayed when the widget is in an enabled state. */
        onText?: string;
        /** Specifies the text displayed when the widget is switched off. */
        switchedOffText?: string;
        /** Specifies the text displayed when the widget is switched on. */
        switchedOnText?: string;
        /** A Boolean value specifying whether the current switch state is "On" or "Off". */
        value?: boolean;


        //onValueChanged?(callback: (e: object) => void): void;
        onValueChanged?: (e: object) => void;
    }
    /** The Switch is a widget that can be in two states: "On" and "Off". */
    export class dxSwitch extends Editor {
        //constructor(element: Element, options?: dxSwitchOptions)
        //constructor(element: JQuery, options?: dxSwitchOptions)

       /** Updates the value of a single option. */
        option(optionName: string, optionValue: any): void;

        /** Updates the values of several options. */
        option(options: dxSwitchOptions): void;


        //every(callback: (item: Object, index: number, source: ObservableArray) => boolean): boolean;
    }
}
