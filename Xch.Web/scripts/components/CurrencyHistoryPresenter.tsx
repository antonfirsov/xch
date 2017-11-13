import * as React from "react";
import * as ReactDOM from "react-dom";
import ReactHighstock = require('react-highcharts/ReactHighstock.src');

import { Ajax } from "../utils/Ajax";
import { IUriProps, ICurrencyHistory, flattenCurrencyHistoryData, extractHighChartSeries } from "../utils/Data";

interface ICurrencyHistoryPresenterState {
    history? : ICurrencyHistory;
}

export class CurrencyHistoryPresenter extends React.Component<IUriProps, ICurrencyHistoryPresenterState> {
    render(): React.ReactNode {
        if (!this.state || !this.state.history) {
            return "";
        }

        //var data = flattenCurrencyHistoryData(this.state.history, 2);
        var filter = (s: string) => s === "PHP" || s === "CZK" || s === "RUB";

        var series = extractHighChartSeries(this.state.history, filter);

        var config = {
            rangeSelector: {
                selected: 1
            },
            title: {
                text: 'Currency history'
            },
            series: series,
            chart: {
                height: 700
            }
        };

        return (
            <div className="currency-history-presenter">
                <ReactHighstock config={config} />
            </div>
        );
    }

    componentDidMount(): void {
        Ajax.get<ICurrencyHistory>(this.props.uri).then(history => {
            this.setState({history: history});
        });
    }
}