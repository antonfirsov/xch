export interface IUriProps {
    uri: string;
}

export interface ICurrencyConverterParameters {
    sourceAmount: number;
    sourceCurrencyCode: string;
    destCurrencyCode: string;
}

export interface ICurrencyHistory {
    codes: string[];
    dates: string[];
    data: number[][];
}

export interface IHighchartsSerie {
    name: string;
    data: number[][];
    tooltip: any;
}

export function flattenCurrencyHistoryData(history: ICurrencyHistory, idx: number): number[][] {
    var keys = history.dates.map((d) => new Date(d).getTime());
    var vals = history.data[idx];

    return keys.map((e, i) => [e, vals[i]]);
}

export function extractHighChartSeries(history: ICurrencyHistory, f: (string) => boolean): IHighchartsSerie[] {
    return history.codes.map((e, i) => {
        var s: IHighchartsSerie = {
            name: e,
            data: flattenCurrencyHistoryData(history, i),
            tooltip : { valueDecimals: 2}
        };
        return s;
    }).filter(s => f(s.name));
}