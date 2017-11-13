import * as React from "react";
import * as ReactDOM from "react-dom";
import ReactHighstock = require('react-highcharts/ReactHighstock.src');

import { Ajax } from "../utils/Ajax";
import { IUriProps, ICurrencyHistory } from "../utils/Data";

interface ICurrencyHistoryPresenterState {
    history? : ICurrencyHistory;
}

export class CurrencyHistoryPresenter extends React.Component<IUriProps, ICurrencyHistoryPresenterState> {
    render(): React.ReactNode {
        if (!this.state.history) {
            return "";
        }

        var keys = this.state.history.dates.map((d) => new Date(d).getTime());
        var vals = this.state.history.data[0];

        var data = keys.map((e, i) => [e, vals[i]]);

        return (
            <div>
            </div>
        );
    }

    componentDidMount(): void {
        Ajax.get<ICurrencyHistory>(this.props.uri).then(history => {
            this.setState({history: history});
        });
    }
}