
import * as React from "react";
import { Ajax } from "../utils/Ajax";
import { IUriProps } from "../utils/Data";

interface ICurrencyCodes {
    codes : string[];
}

interface ICurrencyPickerProps  {
    uri?: string;
    codes?:string[];
    defaultSelected? : string;
    onChanged?: (string) => void;
}

export class CurrencyPicker extends React.Component<ICurrencyPickerProps, ICurrencyCodes> {
    constructor(props: ICurrencyPickerProps) {
        super(props);
        
        var codesArr = props.codes || [];

        this.state = { codes: codesArr };
    }

    componentWillReceiveProps(nextProps: Readonly<ICurrencyPickerProps>, nextContext): void {
        if (nextProps.codes) {
            this.setState({ codes: nextProps.codes});
        }
    }

    private get cProps(): ICurrencyPickerProps {
        return this.props as ICurrencyPickerProps;
    }
    
    render(): React.ReactNode {
        var makeItem = c => <option value={c} selected={c === this.props.defaultSelected}>{c}</option>;

        return <select onChange={this.changedHandler.bind(this)} >
                {this.state.codes.map(makeItem)}
               </select>;
    }

    private changedHandler(e) {
        if (this.cProps.onChanged != null) {
            var target = e.target as HTMLSelectElement;
            var value = target.selectedOptions.item(0).value;
            this.cProps.onChanged(value);
        }
    }

    componentDidMount(): void {
        if (this.cProps.uri) {
            Ajax.get<string[]>(this.cProps.uri).then(result => {
                this.setState({ codes: result });
            });
        }
    }
}