import * as React from "react";
import * as ReactDOM from "react-dom";
import ReactHighstock = require('react-highcharts/ReactHighstock.src');

import * as Bs from 'react-bootstrap';

import { Ajax } from "../utils/Ajax";
import { IUriProps, ICurrencyHistory, flattenCurrencyHistoryData, extractHighChartSeries } from "../utils/Data";

interface ICurrencyHistoryPresenterState {
    history?: ICurrencyHistory;
    codesShown: string[];
}

//interface ICurrencyButtonProps {
//    code: string;
//}

//interface ICurrencyButtonState {
//    enabled: boolean;
//}

//class CurrencyButton extends React.Component<ICurrencyButtonProps, ICurrencyButtonState> {
//    render(): React.ReactNode {
//        return <Bs.ToggleButton>{this.props.code}</Bs.ToggleButton> ;
//    }
//}

export class CurrencyHistoryPresenter extends React.Component<IUriProps, ICurrencyHistoryPresenterState> {
    render(): React.ReactNode {
        if (!this.state || !this.state.history) {
            return "";
        }
        
        var filter = (s: string) => this.state.codesShown.some(x => x === s);

        var series = extractHighChartSeries(this.state.history, filter);

        var config = {
            rangeSelector: {
                selected: 1
            },
            series: series,
            chart: {
                height: 700
            }
        };

        return (
            <div className="currency-history-presenter">
                <Bs.ButtonToolbar>
                    <Bs.ToggleButtonGroup type="checkbox" value={this.state.codesShown} onChange={this.handleCurrencyButtonsChanged.bind(this)}>
                        {this.state.history.codes.map(c => this.getCurrencyButton(c))}
                    </Bs.ToggleButtonGroup>
                    <ReactHighstock config={config}/>
                </Bs.ButtonToolbar>
            </div>
        );
    }

    private getCurrencyButton(code: string): React.ReactNode {
        return <Bs.ToggleButton value={code}>{code}</Bs.ToggleButton>;
    }

    private handleCurrencyButtonsChanged(values: string[]): void {
        this.setState({ codesShown: values});
    }

    componentDidMount(): void {
        Ajax.get<ICurrencyHistory>(this.props.uri).then(history => {
            var defaultCodes: string[] = [];
            if (history.codes.some(x => x === 'USD')) {
                defaultCodes.push('USD');
            }
            
            this.setState({ history: history, codesShown: defaultCodes});
        });
    }
}