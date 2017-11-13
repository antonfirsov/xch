import * as React from "react";
import { Ajax } from "../utils/Ajax";
import { IUriProps, ICurrencyConverterParameters } from "../utils/Data";
import {CurrencyPicker} from "./CurrencyPicker";
import { isUndefined } from "util";

interface ICurrencyConverterProps {
    codesUri: string;
    converterUri: string;
}

interface ICurrencyConverterState {
    codes: string[];
    currentSource?: string;
    currentDest?: string;

    sourceAmountString: string;
    sourceAmount?:number;
    resultAmount?: number;
}

type StateUpdater<T> = (state: T) => void;

export class CurrencyConverter extends React.Component<ICurrencyConverterProps, ICurrencyConverterState> {

    constructor(props: ICurrencyConverterProps) {
        super(props);
        this.state = { codes: [], sourceAmountString: "1", sourceAmount: 1 };
    }

    render(): React.ReactNode {
        return <form onSubmit={this.handleSubmit.bind(this)}>
                   <label>From: &nbsp;</label>

                   <CurrencyPicker
                       codes={this.state.codes}
                       defaultSelected={this.state.currentSource} onChanged={this.handleSourceChanged.bind(this)} />

                   <label>To: &nbsp;</label>
            
                   <CurrencyPicker
                       codes={this.state.codes}
                       defaultSelected={this.state.currentDest} onChanged={this.handleDestChanged.bind(this)} />

                   <label>Amount: &nbsp;</label>
                   <input value={this.state.sourceAmountString} onChange={this.handleInputChanged.bind(this)} />
                   
                   <button type="submit" disabled={!this.conversionEnabled}>Convert</button>

                   {this.renderResultLabel()}
            </form>;
    }

    private get conversionEnabled(): boolean {
        return this.state.sourceAmount != null && !isUndefined(this.state.sourceAmount);
    }

    private renderResultLabel(): React.ReactNode {
        if (!this.state.resultAmount) return "";

        return <label>&nbsp; Result: {this.state.resultAmount.toFixed(3)}</label>;
    }

    componentDidMount(): void {
        Ajax.get<string[]>(this.props.codesUri).then(result => {
            var first = result.length > 0 ? result[0] : null;
            this.setState({ codes: result, currentSource: first, currentDest: first });
        });
    }

    private handleSubmit(e): void {
        e.preventDefault();

        if (!this.state.currentSource || !this.state.currentDest || !this.conversionEnabled) return;
        
        var parameters: ICurrencyConverterParameters = {
            sourceAmount: this.state.sourceAmount,
            sourceCurrencyCode: this.state.currentSource,
            destCurrencyCode: this.state.currentDest
        };
        
        Ajax.get<number>(this.props.converterUri, parameters).then(result => {
            this.setState({ resultAmount: result});
        });
    }

    private handleInputChanged(e): void {
        var target = e.target as HTMLInputElement;
        var val = parseFloat(target.value);
        if (!val || val === NaN) {
            val = null;
        }
        this.setState({ sourceAmountString: target.value, sourceAmount: val });
        //this.forceUpdate();
    }

    private handleSourceChanged(c: string): void {
        this.setState({ currentSource: c});
    }

    private handleDestChanged(c: string): void {
        this.setState({ currentDest: c });
    }
}