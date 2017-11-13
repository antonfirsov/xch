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

export function flattenCurrencyHistoryData(history: ICurrencyHistory): number[][] {
    var keys = history.dates.map((d) => new Date(d).getTime());
    var vals = history.data[0];

    return keys.map((e, i) => [e, vals[i]]);
}