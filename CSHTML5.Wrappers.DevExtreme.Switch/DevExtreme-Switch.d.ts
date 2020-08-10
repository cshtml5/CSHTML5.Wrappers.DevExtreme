declare module DevExpress.ui {
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
        /** A function that is executed after the widget's value is changed. */
        onValueChanged?: (e: object) => void;
    }

    /** The Switch is a widget that can be in two states: "On" and "Off". */
    export class dxSwitch {
       /** Updates the value of a single option. */
        option(optionName: string, optionValue: any): void;

        /** Updates the values of several options. */
        option(options: dxSwitchOptions): void;
    }
}
